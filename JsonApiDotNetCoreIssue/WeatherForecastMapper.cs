using JsonApiDotNetCore.Models;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecastMapper : IResourceMapper
    {
        public TDestination Map<TDestination>(object source)
        {
            return default;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Map<TDestination>(source);
        }
    }
}