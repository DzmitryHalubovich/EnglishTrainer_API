using AutoMapper;
using EnglishTrainer.Contracts;
using EnglishTrainer.Contracts.Logger;
using EnglishTrainer.Entities.DTO;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
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
    }
}
