using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Services;
using Microsoft.Extensions.Logging;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecastService : EntityResourceService<WeatherForecast, int>
    {
        public WeatherForecastService(IJsonApiContext jsonApiContext,
                                      IEntityRepository<WeatherForecast, int> entityRepository,
                                      ILoggerFactory loggerFactory = null,
                                      IResourceHookExecutor hookExecutor = null) : base(jsonApiContext, entityRepository, loggerFactory, hookExecutor)
        {
        }
    }
}
