using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
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
        public async Task GetValue_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/api/values/5");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
