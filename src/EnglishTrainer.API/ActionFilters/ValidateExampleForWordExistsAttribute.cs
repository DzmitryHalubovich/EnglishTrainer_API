using EnglishTrainer.LoggerService;
using EnglishTrainer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnglishTrainer.API.ActionFilters
{
    public class ValidateExampleForWordExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryManager _repository;

        public ValidateExampleForWordExistsAttribute(ILoggerManager loggerManager, IRepositoryManager repository)
        {
            _loggerManager=loggerManager;
            _repository=repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;

            var wordId = (Guid)context.ActionArguments["wordId"];
            var word = await _repository.Word.GetAsync(wordId, false);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {wordId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            var id = (Guid)context.ActionArguments["id"];
            var example = await _repository.Example.GetAsync(wordId,id, trackChanges);

            if (example == null)
            {
                _loggerManager.LogInfo($"Example with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("example", example);
                await next();
            }

        }
    }
}
