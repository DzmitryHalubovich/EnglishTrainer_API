using AutoMapper;
using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.DTO.Update;
using EnglishTrainer.Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using EnglishTrainer.LoggerService;

namespace EnglishTrainer.API.Controllers
{
    [Route("api/dictionary/{wordId}/examples")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        #region Constructor and DI propherties
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public ExampleController(IRepositoryManager repository,
            ILoggerManager loggerManager, IMapper mapper)
        {
            _repository=repository;
            _loggerManager=loggerManager;
            _mapper=mapper;
        }
        #endregion

        #region Get "..." Get all examples for word method
        [HttpGet]
        public async Task<IActionResult> GetAllForWord(Guid wordId)
        {
            //TODO перепроверить все названия методов в сервисах что бы были адекватными
            //и соответсвовали функционалу

            //сначала определяемся со словом для которого ищем примеры
            var word = await _repository.Word.GetAsync(wordId, trackChanges: false);

            //если не слово найдено
            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            //Ищем примеры по слову
            var examples = await _repository.Example.GetAllAsync(wordId, trackChanges: false);

            var examplesDto = _mapper.Map<IEnumerable<ExampleReadDTO>>(examples);

            return Ok(examplesDto);
        }
        #endregion

        #region Get ".../{id}" Get single example for word
        [HttpGet("{id}", Name = "GetExampleById")]
        public async Task<IActionResult> GetSingleForWord(Guid wordId, Guid id)
        {
            var word = await _repository.Word.GetAsync(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var example = await _repository.Example.GetAsync(wordId, id, trackChanges: false);

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
        public async Task<IActionResult> CreateExampleForWord(Guid wordId,
            [FromBody] ExampleCreateDTO example)
        {
            if (example == null)
            {
                _loggerManager.LogError("ExampleCreateDTO object sent from client is null.");
                return BadRequest("ExampleCreateDTO object is null");
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the ExampleCreateDTO object");
                return UnprocessableEntity(ModelState); 
            }

            var word = await _repository.Word.GetAsync(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleEntity = _mapper.Map<Example>(example);

            _repository.Example.CreateForWord(wordId, exampleEntity);
            _repository.SaveAsync();

            var exampleToReturn = _mapper.Map<ExampleReadDTO>(exampleEntity);

            return CreatedAtRoute("GetExampleById",
                new { wordId, id = exampleToReturn.Id }, exampleToReturn);

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExampleForWord(Guid wordId, Guid Id)
        {
            var word = await _repository.Word.GetAsync(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleForWord = await _repository.Example.GetAsync(wordId, Id, trackChanges: false);
            if (exampleForWord == null)
            {
                _loggerManager.LogInfo($"Example with id: {Id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Example.DeleteExample(exampleForWord);
            _repository.SaveAsync();

            return NoContent();
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExampleForWord(Guid wordId, Guid id, [FromBody] ExampleUpdateDTO example)
        {
            if (example == null)
            {
                _loggerManager.LogError("ExampleUpdateDTO object sent from client is null.");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the ExampleUpdateDTO object");
                return UnprocessableEntity(ModelState);
            }

            var word = await _repository.Word.GetAsync(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleEntity = await _repository.Example.GetAsync(wordId, id, trackChanges: true);

            if (exampleEntity == null)
            {
                _loggerManager.LogInfo($"Example with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(example, exampleEntity);
            _repository.SaveAsync();

            return NoContent();
        }



        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateExampleForWord(Guid wordId, Guid id, 
            [FromBody] JsonPatchDocument<ExampleUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                _loggerManager.LogError("patchDoc object sent client is null.");
                return BadRequest("patchDoc object is null");
            }

            var word = await _repository.Word.GetAsync(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var exampleEntity = await _repository.Example.GetAsync(wordId, id, trackChanges: true);

            if (exampleEntity == null)
            {
                _loggerManager.LogInfo($"Example with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            //Map from Example to ExampleUpdateDTO cause JPD can can apply only to the ExampleUpdateDTO type
            var exampleToPatch = _mapper.Map<ExampleUpdateDTO>(exampleEntity);

            patchDoc.ApplyTo(exampleToPatch, ModelState); //Apply changes

            TryValidateModel(exampleToPatch);

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(exampleToPatch, exampleEntity);
            _repository.SaveAsync();

            return NoContent();
        }

    }
}
