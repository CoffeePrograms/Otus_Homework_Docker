using Homework4.Models;

namespace Homework4.Extensions
{
    public static class WeatherForecastExtensions
    {
        public static ShortResponse ToShortResponse(this WeatherForecast entity)
        {
            return new ShortResponse
            {
                Date = entity.Date,
                TemperatureC = entity.TemperatureC,
                Summary = entity.Summary,
            };
        }
    }
}
