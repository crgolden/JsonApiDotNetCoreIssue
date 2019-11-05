using System;
using System.Linq;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Services;
using Microsoft.EntityFrameworkCore;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecastRepository : DefaultEntityRepository<WeatherForecast>
    {
        private readonly DbSet<WeatherForecast> _dbSet;

        public WeatherForecastRepository(
            IJsonApiContext jsonApiContext,
            IDbContextResolver contextResolver,
            ResourceDefinition<WeatherForecast> resourceDefinition = null)
        : base(jsonApiContext, contextResolver, resourceDefinition)
        {
            _dbSet = contextResolver.GetDbSet<WeatherForecast>();
        }

        public override IQueryable<WeatherForecast> Get()
        {
            Console.WriteLine();
            return _dbSet;
        }
    }
}
