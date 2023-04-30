using EnglishTrainer.LoggerService;
using EnglishTrainer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnglishTrainer.API.ActionFilters
{
    public class ValidateWordExistAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;

        public ValidateWordExistAttribute(ILoggerManager loggerManager, IRepositoryManager repository)
        {
            _loggerManager=loggerManager;
            _repository=repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var word = await _repository.Word.GetAsync(id, trackChanges);

            if (word == null)
            {
                _loggerManager.LogInfo($"Word with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("word", word);
                await next();
            }
        }
    }
}
