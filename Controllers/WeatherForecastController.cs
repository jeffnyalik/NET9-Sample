using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Webbs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase 
    {
        private readonly ILoggerManager logger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ILoggerManager logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            logger.logInfo("Here is info message from the controller.");
            logger.logDebug("Here is debug message from the controller.");
            logger.logWarning("Here is warn message from the controller.");
            logger.logError("Here is error message from the controller.");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {   

                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
