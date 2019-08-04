using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AuthSample.Tests
{
    public class ValueControllerTest : IClassFixture<TestWebServerFixture>
    {
        private readonly TestWebServerFixture _testServerFixture;
        public ValueControllerTest(TestWebServerFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
        }

        private HttpClient Arrange()
        {
            return _testServerFixture.CreateDefaultClient();
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
