using AutoMapper;
using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.DTO.Update;
using EnglishTrainer.Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using EnglishTrainer.LoggerService;
using EnglishTrainer.API.ActionFilters;
using System.ComponentModel.Design;
using EnglishTrainer.Entities.RequestFeatures;
using Newtonsoft.Json;

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
        public async Task<IActionResult> GetExamplesForWord(Guid wordId, [FromQuery] ExampleParameters exampleParameters)
        {
            var word = await _repository.Word.GetAsync(wordId, trackChanges: false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                return NotFound();
            }

            var examples = await _repository.Example.GetExamplesAsync(wordId, exampleParameters ,trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(examples.MetaData));

            var examplesDto = _mapper.Map<IEnumerable<ExampleReadDTO>>(examples);

            return Ok(examplesDto);
        }
        #endregion



        #region Get ".../{id}" Get single example for word
        [HttpGet("{id}", Name = "GetExampleById")]
        public async  Task<IActionResult> GetExampleForWord(Guid wordId, Guid id)
        {
            var word = await _repository.Word.GetAsync(wordId, trackChanges: false); 
            if (word == null) 
            { 
                _loggerManager.LogInfo($"Company with id: {wordId} doesn't exist in the database."); 
                return NotFound(); 
            }

            var example = await _repository.Example.GetAsync(wordId,id,false);

            var exampleDto = _mapper.Map<ExampleReadDTO>(example);
            return Ok(exampleDto);
        }
        #endregion



        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExampleForWord(Guid wordId,
            [FromBody] ExampleCreateDTO example)
        {
            var exampleEntity = _mapper.Map<Example>(example);

            _repository.Example.CreateForWord(wordId, exampleEntity);
            await _repository.SaveAsync();

            var exampleToReturn = _mapper.Map<ExampleReadDTO>(exampleEntity);

            return CreatedAtRoute("GetExampleById",
                new { wordId, id = exampleToReturn.Id }, exampleToReturn);
        }



        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateExampleForWordExistsAttribute))]
        public async Task<IActionResult> DeleteExampleForWord(Guid wordId, Guid Id)
        {
            var exampleForWord = HttpContext.Items["example"] as Example;

            _repository.Example.DeleteExample(exampleForWord);
            await _repository.SaveAsync();

            return NoContent();
        }



        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateExampleForWordExistsAttribute))]
        public async Task<IActionResult> UpdateExampleForWord(Guid wordId, Guid id, [FromBody] ExampleUpdateDTO example)
        {
            var exampleEntity = HttpContext.Items["example"] as Example;

            _mapper.Map(example, exampleEntity);
            await _repository.SaveAsync();

            return NoContent();
        }



        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateExampleForWordExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateExampleForWord(Guid wordId, Guid id, 
            [FromBody] JsonPatchDocument<ExampleUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                _loggerManager.LogError("patchDoc object sent client is null.");
                return BadRequest("patchDoc object is null");
            }

            var exampleEntity = HttpContext.Items["example"] as Example;

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
            await _repository.SaveAsync();

            return NoContent();
        }

    }
}
