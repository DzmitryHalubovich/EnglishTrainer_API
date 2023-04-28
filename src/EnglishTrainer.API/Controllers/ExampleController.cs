using AutoMapper;
using EnglishTrainer.Contracts;
using EnglishTrainer.Contracts.Logger;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.DTO.Update;
using EnglishTrainer.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTrainer.API.Controllers
{
    [Route("api/dictionary/{wordId}/examples")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        #region Constructor and DI propherties
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
        #endregion

        #region Get "..." Get all examples for word method
        [HttpGet]
        public IActionResult GetAllForWord(Guid wordId)
        {
            //TODO перепроверить все названия методов в сервисах что бы были адекватными
            //и соответсвовали функционалу

            //сначала определяемся со словом для которого ищем примеры
            var word = _serviceManager.Word.GetWord(wordId, trackChanges: false);

            //если не слово найдено
            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            //Ищем примеры по слову
            var examples = _serviceManager.Example.GetAll(wordId, trackChanges: false);

            var examplesDto = _mapper.Map<IEnumerable<ExampleReadDTO>>(examples);

            return Ok(examplesDto);
        }
        #endregion

        #region Get ".../{id}" Get single example for word
        [HttpGet("{id}", Name = "GetExampleById")]
        public IActionResult GetSingleForWord(Guid wordId, Guid id)
        {
            var word = _serviceManager.Word.GetWord(wordId, trackChanges: false);

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

            var exampleDto = _mapper.Map<ExampleReadDTO>(example);
            return Ok(exampleDto);
        }
        #endregion

        [HttpPost]
        public IActionResult CreateExampleForWord(Guid wordId,
            [FromBody] ExampleCreateDTO example)
        {
            if (example == null)
            {
                _loggerManager.LogError("ExampleCreateDTO object sent from client is null.");
                return BadRequest("ExampleCreateDTO object is null");
            }

            var word = _serviceManager.Word.GetWord(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleEntity = _mapper.Map<Example>(example);

            _serviceManager.Example.CreateForWord(wordId, exampleEntity);
            _serviceManager.Save();

            var exampleToReturn = _mapper.Map<ExampleReadDTO>(exampleEntity);

            return CreatedAtRoute("GetExampleById",
                new { wordId, id = exampleToReturn.Id }, exampleToReturn);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExampleForWord(Guid wordId, Guid Id)
        {
            var word = _serviceManager.Word.GetWord(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleForWord = _serviceManager.Example.Get(wordId, Id, trackChanges: false);
            if (exampleForWord == null)
            {
                _loggerManager.LogInfo($"Example with id: {Id} doesn't exist in the database.");
                return NotFound();
            }

            _serviceManager.Example.DeleteExample(exampleForWord);
            _serviceManager.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExampleForWord(Guid wordId, Guid id, [FromBody] ExampleUpdateDTO example)
        {
            if (example == null)
            {
                _loggerManager.LogError("ExampleUpdateDTO object sent from client is null.");
                return BadRequest();
            }

            var word = _serviceManager.Word.GetWord(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleEntity = _serviceManager.Example.Get(wordId, id, trackChanges: true);

            if (exampleEntity == null)
            {
                _loggerManager.LogInfo($"Example with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(example, exampleEntity);
            _serviceManager.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateExampleForWord(Guid wordId, Guid id, 
            [FromBody] JsonPatchDocument<ExampleUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                _loggerManager.LogError("patchDoc object sent client is null.");
                return BadRequest("patchDoc object is null");
            }

            var word = _serviceManager.Word.GetWord(wordId, trackChanges: false);
            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleEntity = _serviceManager.Example.Get(wordId, id, trackChanges:true);
            if (exampleEntity == null)
            {
                _loggerManager.LogInfo($"Example with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            //Map from Example to ExampleUpdateDTO cause JPD can can apply only to the ExampleUpdateDTO type
            var exampleToPatch = _mapper.Map<ExampleUpdateDTO>(exampleEntity);

            patchDoc.ApplyTo(exampleToPatch); //Apply changes

            _mapper.Map(exampleToPatch, exampleEntity);
            _serviceManager.Save();

            return NoContent();
        }
    }
}
