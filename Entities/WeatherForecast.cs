using System;
using JsonApiDotNetCore.Models;

namespace JsonApiDotNetCoreIssue
{
    public class WeatherForecast : IIdentifiable<int>
    {
        public int Id { get; set; }

        public string StringId
        {
            get => Id.ToString();
            set => Id = int.Parse(value);
        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
