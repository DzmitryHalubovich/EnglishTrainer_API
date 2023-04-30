using AutoMapper;
using EnglishTrainer.API.ModelBinders;
using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.DTO.Update;
using EnglishTrainer.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using EnglishTrainer.LoggerService;

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
        public async Task<IActionResult> GetAllWords()
        {

            var dictionary = await _repository.Word.GetAllAsync(trackChanges: false);

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
        public async Task<IActionResult> GetWord(Guid id)
        {
            var word = await _repository.Word.GetAsync(id, trackChanges: false);
            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var wordDto = _mapper.Map<WordReadDTO>(word);
            return Ok(wordDto);
        }


        [HttpPost]
        //Данные приходят из тела запроса, а не из URL,так что пишем FromBody
        public async Task<IActionResult> CreateWord([FromBody] WordCreateDTO word)
        {
            //TODO переделать сервисы под репозитории, добавить новые сервисы

            //Если не можем десирелизовать, возвращаем BadRequest
            if (word == null)
            {
                _loggerManager.LogError("WordCreateDTO object from client is null.");
                return BadRequest("WordCreateDTO object is null");
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the WordCreateDTO object");

                //Custom error
                ModelState.AddModelError("error", "По голове себе постучи, дебил!");
                return UnprocessableEntity(ModelState);
            }

            var wordEntity = _mapper.Map<Word>(word);

            _repository.Word.CreateWord(wordEntity);
            _repository.SaveAsync();

            var wordToReturn = _mapper.Map<WordReadDTO>(wordEntity);

            //Call another method from controller to represent a new item 
            return CreatedAtRoute("WordById", new { id = wordToReturn.Id },
                wordToReturn);
        }



        [HttpPost("collection")]
        public async Task<IActionResult> CreateWordsCollection(
            [FromBody] IEnumerable<WordCreateDTO> wordsCollection)
        {
            if (wordsCollection == null)
            {
                _loggerManager.LogError("Words collection sent from client is null.");
                return BadRequest("Words collection is null");
            }

            var wordEntities = _mapper.Map<IEnumerable<Word>>(wordsCollection);

            foreach (var word in wordEntities)
            {
                _repository.Word.CreateWord(word);
            }

            _repository.SaveAsync();

            var wordsCollectionToReturn = _mapper.Map<IEnumerable<WordReadDTO>>(wordEntities);

            var ids = string.Join(",", wordsCollectionToReturn.Select(x=>x.Id));

            return CreatedAtRoute("WordsCollection", new { ids }, wordsCollectionToReturn);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(Guid id)
        {
            var word = await _repository.Word.GetAsync(id, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Word.DeleteWord(word);
            _repository.SaveAsync();

            return NoContent();
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWord(Guid id, [FromBody] WordUpdateDTO word)
        {
            if (word == null)
            {
                _loggerManager.LogError("WordUpdateDTO object sent from client is null.");
                return BadRequest("WordUpdateDTO object is null");
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the WordUpdateDTO object");
                return UnprocessableEntity(ModelState);
            }

            var wordEntity = await _repository.Word.GetAsync(id, trackChanges: true);

            if (wordEntity == null)
            {
                _loggerManager.LogInfo($"Word with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(word, wordEntity);
            _repository.SaveAsync();

            return NoContent();
        }
    }
}
