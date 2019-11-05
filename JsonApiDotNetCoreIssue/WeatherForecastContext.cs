using Microsoft.EntityFrameworkCore;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options)
            : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
