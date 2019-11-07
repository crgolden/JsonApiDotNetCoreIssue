using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Extensions;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JsonApiDotNetCoreIssue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<WeatherForecastContext>(options =>
                {
                    options.UseInMemoryDatabase("WeatherForecasts");
                })
                .AddScoped<IResourceService<WeatherForecast, int>, WeatherForecastService>()
                .AddScoped<IEntityRepository<WeatherForecast>, WeatherForecastRepository>()
                .AddScoped<IResourceMapper, WeatherForecastMapper>()
                .AddScoped<ResourceDefinition<WeatherForecast>, WeatherForecastDefinition>()
                .AddJsonApi<WeatherForecastContext>(options =>
                {
                    options.EnableResourceHooks = true;
                    options.BuildResourceGraph(builder => builder.AddResource<WeatherForecastModel>());
                }, services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_2));

            // make sure this override is added AFTER you call AddJsonApi( .. ), because else it is overwritten by it
            services.AddSingleton(typeof(IHooksDiscovery<>), typeof(HooksDiscoveryOverride<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WeatherForecastContext context)
        {
            context.Database.EnsureCreated();
            if (context.WeatherForecasts.Any() == false)
            {
                context.WeatherForecasts.Add(new WeatherForecast
                {
                    Summary = "Freezing!"
                });
                context.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseJsonApi();
        }
    }
}
