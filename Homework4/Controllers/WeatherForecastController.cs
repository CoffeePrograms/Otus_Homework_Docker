using Homework4.Extensions;
using Homework4.Models;
using Homework4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IService<WeatherForecast> _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IService<WeatherForecast> service,
            ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecasts")]
        public async Task<ActionResult<IEnumerable<ShortResponse>>> GetAsync()
        {
            var result = await _service.GetAllAsync();
            if (result == null)
            {
                return NotFound();
            }

            var response = result.Select(c => c.ToShortResponse()).ToList();
            return Ok(response);
        }
    }
}
