using AutoMapper;
using EnglishTrainer.Contracts;
using EnglishTrainer.Contracts.Logger;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTrainer.API.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public DictionaryController(IServiceManager serviceManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _serviceManager=serviceManager;
            _loggerManager=loggerManager;
            _mapper=mapper;
        }

        [HttpGet]
        public IActionResult GetWords()
        {

            var dictionary = _serviceManager.Word.GetAll(trackChanges: false);

            var dictionaryDTO = _mapper.Map<IEnumerable<WordDTO>>(dictionary);

            return Ok(dictionaryDTO);

        }


        //Name - дает название URL-у метода, в строке 77 по названию мы вызываем этот метод по его названию
        [HttpGet("{id}", Name = "WordById")]
        public IActionResult GetWord(Guid id)
        {
            var word = _serviceManager.Word.GetWord(id, trackChanges: false);
            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var wordDto = _mapper.Map<WordDTO>(word);
            return Ok(wordDto);
        }


        [HttpPost]
        //Данные приходят из тела запроса, а не из URL,так что пишем FromBody
        public IActionResult CreateWord([FromBody] WordCreateDTO word)
        {
            //TODO переделать сервисы под репозитории, добавить новые сервисы


            //Если не можем десирелизовать, возвращаем BadRequest
            if (word == null)
            {
                _loggerManager.LogError("WordCreateDTO object from client is null.");
                return BadRequest("WordCreateDTO object is null");
            }

            var wordEntity = _mapper.Map<Word>(word);

            _serviceManager.Word.CreateWord(wordEntity);
            _serviceManager.Save();

            var wordToReturn = _mapper.Map<WordDTO>(wordEntity);

            //Call another method from controller to represent a new item 
            return CreatedAtRoute("WordById", new { id = wordToReturn.Id },
                wordToReturn);
        }
    }
}
