using AutoMapper;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.RequestFeatures;
using EnglishTrainer.LoggerService;
using EnglishTrainer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTrainer.API.Controllers
{
    [Route("api/irregular_verb")]
    [ApiController]
    public class IrregularVerbController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;

        public IrregularVerbController(IRepositoryManager repository, ILoggerManager loggerManager)
        {
            _repository=repository;
            _loggerManager=loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetVerbs()
        {
            var verbs = await _repository.IrregularVerb.GetVerbsAsync( trackChanges: false);

            return Ok(verbs);
        }
    }
}
