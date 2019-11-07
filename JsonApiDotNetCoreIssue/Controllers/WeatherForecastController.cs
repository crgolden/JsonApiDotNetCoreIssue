using System.Threading.Tasks;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace JsonApiDotNetCoreIssue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [DisableRoutingConvention]
    public class WeatherForecastController : BaseJsonApiController<WeatherForecast>
    {
        private readonly IResourceService<WeatherForecast, int> _resourceService;

        public WeatherForecastController(
            IJsonApiContext jsonApiContext,
            IResourceService<WeatherForecast, int> resourceService)
            : base(jsonApiContext, resourceService)
        {
            _resourceService = resourceService;
        }
        
        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var resources = await _resourceService.GetAsync();
            return Ok(resources);
        }
    }
}
