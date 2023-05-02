using AutoMapper;
using EnglishTrainer.API.ModelBinders;
using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.DTO.Update;
using EnglishTrainer.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using EnglishTrainer.LoggerService;
using EnglishTrainer.API.ActionFilters;
using EnglishTrainer.Entities.RequestFeatures;

namespace EnglishTrainer.API.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public DictionaryController(IRepositoryManager repository, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository=repository;
            _loggerManager=loggerManager;
            _mapper=mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetWords([FromQuery] WordParameters wordParameters)
        {
            var dictionary = await _repository.Word.GetWordsAsync(wordParameters, trackChanges: false);

            var dictionaryDTO = _mapper.Map<IEnumerable<WordReadDTO>>(dictionary);

            return Ok(dictionaryDTO);
        }



        [HttpGet("collection/({ids})", Name = "WordsCollection")]
        public async Task<IActionResult> GetWordsCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _loggerManager.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var wordsEntities = await _repository.Word.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != wordsEntities.Count())
            {
                _loggerManager.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var wordsToReturn = _mapper.Map<IEnumerable<WordReadDTO>>(wordsEntities);
            return Ok(wordsToReturn);
        }



        //Name - дает название URL-у метода, в строке 77 по названию мы вызываем этот метод по его названию
        [HttpGet("{id}", Name = "WordById")]
        [ServiceFilter(typeof(ValidateWordExistAttribute))]
        public IActionResult GetWord(Guid id)
        {
            var word = HttpContext.Items["word"] as Word;

            var wordDto = _mapper.Map<WordReadDTO>(word);
            return Ok(wordDto);
        }



        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        //Данные приходят из тела запроса, а не из URL,так что пишем FromBody
        public async Task<IActionResult> CreateWord([FromBody] WordCreateDTO word)
        {
            var wordEntity = _mapper.Map<Word>(word);

            _repository.Word.CreateWord(wordEntity);
            await _repository.SaveAsync();

            var wordToReturn = _mapper.Map<WordReadDTO>(wordEntity);

            //Call another method from controller to represent a new item 
            return CreatedAtRoute("WordById", new { id = wordToReturn.Id },
                wordToReturn);
        }



        [HttpPost("collection")]
        public async Task<IActionResult> CreateWordsCollection(
            [FromBody] IEnumerable<WordCreateDTO> wordsCollection)
        {
            var wordEntities = _mapper.Map<IEnumerable<Word>>(wordsCollection);

            foreach (var word in wordEntities)
            {
                _repository.Word.CreateWord(word);
            }

            await _repository.SaveAsync();

            var wordsCollectionToReturn = _mapper.Map<IEnumerable<WordReadDTO>>(wordEntities);

            var ids = string.Join(",", wordsCollectionToReturn.Select(x=>x.Id));

            return CreatedAtRoute("WordsCollection", new { ids }, wordsCollectionToReturn);
        }



        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateWordExistAttribute))]
        public async Task<IActionResult> DeleteWord(Guid id)
        {
            var word = HttpContext.Items["word"] as Word;

            _repository.Word.DeleteWord(word);
            await _repository.SaveAsync();

            return NoContent();
        }



        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateWordExistAttribute))]
        public async Task<IActionResult> UpdateWord(Guid id, [FromBody] WordUpdateDTO word)
        {
            var wordEntity = HttpContext.Items["word"] as Word;

            _mapper.Map(word, wordEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
