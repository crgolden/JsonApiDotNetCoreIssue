using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace JsonApiDotNetCoreIssue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [DisableRoutingConvention]
    public class WeatherForecastController : JsonApiController<WeatherForecast>
    {
        private readonly IResourceService<WeatherForecast, int> _resourceService;

        public WeatherForecastController(
            IJsonApiContext jsonApiContext,
            IResourceService<WeatherForecast, int> resourceService)
            : base(jsonApiContext, resourceService)
        {
            _resourceService = resourceService;
        }
    }
}
