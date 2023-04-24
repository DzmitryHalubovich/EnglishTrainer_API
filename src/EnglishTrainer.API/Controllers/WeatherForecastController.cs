using EnglishTrainer.Contracts;
using EnglishTrainer.Contracts.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTrainer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly IServiceManager _serviceManager;
    public WeatherForecastController(ILoggerManager logger, IServiceManager serviceManager)
    {
        _logger = logger;
        _serviceManager=serviceManager;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet] 
    public ActionResult<IEnumerable<string>> Get() 
    {
        _serviceManager.Word.ToString();
        _serviceManager.IrregularVerb.ToString();
        _serviceManager.Example.ToString();


        //_logger.LogInfo("Here is info message from our values controller.");
        //_logger.LogDebug("Here is debug message from our values controller.");
        //_logger.LogWarn("Here is warn message from our values controller.");
        //_logger.LogError("Here is an error message from our values controller.");

        return new string[] { "value1", "value2" };
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    //public IEnumerable<WeatherForecast> Get()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}
}
