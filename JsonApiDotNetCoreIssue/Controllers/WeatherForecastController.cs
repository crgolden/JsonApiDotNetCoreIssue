using System.Threading.Tasks;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace JsonApiDotNetCoreIssue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [DisableRoutingConvention]
    public class WeatherForecastController : BaseJsonApiController<WeatherForecastModel>
    {
        private readonly IResourceService<WeatherForecastModel, int> _resourceService;

        public WeatherForecastController(
            IJsonApiContext jsonApiContext,
            IResourceService<WeatherForecastModel, int> resourceService)
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
