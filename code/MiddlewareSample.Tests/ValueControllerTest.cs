using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiddlewareSample.Tests
{
    public class ValueControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFixture;
        public ValueControllerTest(WebApplicationFactory<Startup> _webApplicationFixture)
        {
            this._webApplicationFixture = _webApplicationFixture;
        }

        private HttpClient Arrange()
        {
            return _webApplicationFixture.CreateDefaultClient();
        }

        [Fact]
        public async Task GetValues_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/api/values");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetValue_Exists_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/api/values/1");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await result.Content.ReadAsStringAsync();
            responseContent.Should().Be("value");
        }

        [Fact]
        public async Task GetValue_NotFound_StatusCodeAndResponse_Are404_And_Empty()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/api/values/5");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var responseContent = await result.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseContent);
            responseObject.Should().ContainKey("error");
            var error = responseObject.Value<JObject>("error");
            error.Should().NotBeNull().And.ContainKeys("code", "message");
            var errorCode = error.Value<string>("code");
            errorCode.Should().Be("NotFound");
        }

        [Fact]
        public async Task Post_ValidValue_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var requestObj = new
            {
                name = "TestName",
                age = 22
            };
            var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/values/", request);

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            responseContent.Should().BeEmpty();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Headers.Location.Should().Be("http://localhost/api/Values/2");
        }

        [Fact]
        public async Task Post_InvalidValue_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var requestObj = new
            {
                age = -2
            };
            var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/values/", request);

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            var responseObj = JObject.Parse(responseContent);
            responseObj.Should().ContainKeys("Age", "Name");
            var ageErrors = responseObj.Value<JArray>("Age");
            ageErrors.Should().HaveCount(1);
            ageErrors.First().Value<string>().Should().Be("The field Age must be between 0 and 2147483647.");
            var nameErrors = responseObj.Value<JArray>("Name");
            nameErrors.Should().HaveCount(1);
            nameErrors.First().Value<string>().Should().Be("The Name field is required.");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Headers.Location.Should().BeNull();
        }
    }
}
