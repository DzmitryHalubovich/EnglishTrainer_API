using AutoMapper;
using EnglishTrainer.Contracts.Logger;
using EnglishTrainer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnglishTrainer.Entities.DTO.Read;

namespace EnglishTrainer.API.Controllers
{
    [Route("api/dictionary/{wordId}/examples")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public ExampleController(IServiceManager serviceManager, 
            ILoggerManager loggerManager, IMapper mapper)
        {
            _serviceManager=serviceManager;
            _loggerManager=loggerManager;
            _mapper=mapper;
        }

        [HttpGet]
        public IActionResult GetAllForWord(Guid wordId)
        {
            //TODO перепроверить все названия методов в сервисах что бы были адекватными
            //и соответсвовали функционалу

            //сначала определяемся со словом для которого ищем примеры
            var word = _serviceManager.Word.GetWord(wordId, trackChanges:false);

            //если не слово найдено
            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            //Ищем примеры по слову
            var examples = _serviceManager.Example.GetAll(wordId, trackChanges: false);

            var examplesDto = _mapper.Map<IEnumerable<ExampleDTO>>(examples);

            return Ok(examplesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleForWord(Guid wordId, Guid id)
        {
            var word = _serviceManager.Word.GetWord(wordId, trackChanges:false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var example = _serviceManager.Example.Get(wordId, id, trackChanges: false);

            if (example == null)
            {
                _loggerManager.LogInfo($"Example with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var exampleDto = _mapper.Map<ExampleDTO>(example);
            return Ok(exampleDto);
        }
    }
}
