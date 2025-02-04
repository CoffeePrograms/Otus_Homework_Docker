using Homework4.Models;
using Homework4.Repositories.Interfaces;
using Homework4.Services.Interfaces;

namespace Homework4.Services
{
    public class WeatherForecastService : IService<WeatherForecast>
    {
        private IRepository<WeatherForecast> _repository;

        public WeatherForecastService(
            IRepository<WeatherForecast> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
