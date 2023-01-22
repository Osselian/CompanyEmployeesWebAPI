using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Info message");
            _logger.LogDebug("Debug message");
            _logger.LogWarn("Warning message");
            _logger.LogError("Error message");


            return new string[] { "value1", "value2" };
        }
    }
}