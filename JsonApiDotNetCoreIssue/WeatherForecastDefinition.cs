using System;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCore.Internal;
using JsonApiDotNetCore.Models;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecastDefinition : ResourceDefinition<WeatherForecast>
    {
        public WeatherForecastDefinition(IResourceGraph graph) : base(graph)
        {
        }

        public override void BeforeRead(ResourcePipeline pipeline, bool isIncluded = false, string stringId = null)
        {
            Console.WriteLine(pipeline.ToString());
            base.BeforeRead(pipeline, isIncluded, stringId);
        }
    }
}
