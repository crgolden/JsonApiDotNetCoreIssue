using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCoreIssue;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IntTests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public UnitTest1(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Found()
        {
            // Arrange
            const string id = "1";
            var requestUri = new Uri("weatherForecast", UriKind.Relative);
            Document result;

            // Act
            using (var response = await _httpClient.GetAsync($"{requestUri}/{id}"))
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Document>(responseString);
            }

            // Assert
            Assert.Equal(id, result.Data.Id);
        }

        [Fact]
        public async Task NotFound()
        {
            // Arrange
            const string id = "2";
            var requestUri = new Uri("weatherForecast", UriKind.Relative);
            Document result;
            HttpStatusCode statusCode;

            // Act
            using (var response = await _httpClient.GetAsync($"{requestUri}/{id}"))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Document>(responseString);
                statusCode = response.StatusCode;
            }

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            Assert.Null(result.Data);
        }
    }
}
