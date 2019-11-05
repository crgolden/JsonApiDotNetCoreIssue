using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Services;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecastService : EntityResourceService<WeatherForecastModel, WeatherForecast, int>
    {
        public WeatherForecastService(
            IJsonApiContext jsonApiContext,
            IEntityRepository<WeatherForecast, int> entityRepository,
            IResourceHookExecutor hookExecutor,
            IResourceMapper mapper)
        : base(jsonApiContext, entityRepository, hookExecutor, mapper)
        {
        }
    }
}
