using System.Threading.Tasks;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace JsonApiDotNetCoreIssue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableRoutingConvention]
    public class WeatherForecastController : BaseJsonApiController<WeatherForecast>
    {
        private readonly IGetAllService<WeatherForecast> _resourceService;

        public WeatherForecastController(
            IJsonApiContext jsonApiContext,
            IGetAllService<WeatherForecast> resourceService)
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
