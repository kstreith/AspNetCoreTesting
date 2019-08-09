using AngleSharp;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MvcSample.Tests
{
    public class HomeControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFixture;
        public HomeControllerTest(WebApplicationFactory<Startup> webApplicationFixture)
        {
            _webApplicationFixture = webApplicationFixture;
        }

        private HttpClient Arrange()
        {
            return _webApplicationFixture.CreateDefaultClient();
        }

        [Fact]
        public async Task GetIndex_PageTitle_IsCorrect()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseStream = await result.Content.ReadAsStreamAsync();
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(responseStream));
            var title = document.QuerySelector("title");
            title.TextContent.Should().Be("Home Page - MvcSample");
        }

        [Fact]
        public async Task GetPrivacy_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/home/privacy");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
