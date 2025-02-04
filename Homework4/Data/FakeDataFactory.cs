using Homework4.Models;

namespace Homework4.Data
{
    public class FakeDataFactory
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static IEnumerable<WeatherForecast> WeatherForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
