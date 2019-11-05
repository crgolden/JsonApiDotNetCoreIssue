using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCore.Services;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecastService : IGetAllService<WeatherForecast>
    {
        private readonly IEntityRepository<WeatherForecast> _entityRepository;
        private readonly IResourceHookExecutor _hookExecutor;

        public WeatherForecastService(
            IEntityRepository<WeatherForecast> entityRepository,
            IResourceHookExecutor hookExecutor = null)
        {
            _entityRepository = entityRepository;
            _hookExecutor = hookExecutor;
        }

        public Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            Console.WriteLine();
            _hookExecutor.BeforeRead<WeatherForecast>(ResourcePipeline.Get);
            return Task.FromResult(_entityRepository.Get().AsEnumerable());
        }
    }
}
