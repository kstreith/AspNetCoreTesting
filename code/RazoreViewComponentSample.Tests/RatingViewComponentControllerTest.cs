using AngleSharp;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RazorViewComponentSample.Tests
{
    public class RatingViewComponentControllerTest : IClassFixture<TestWebServerFixture>
    {
        private readonly TestWebServerFixture _webApplicationFixture;
        public RatingViewComponentControllerTest(TestWebServerFixture webApplicationFixture)
        {
            _webApplicationFixture = webApplicationFixture;
        }

        private HttpClient Arrange()
        {
            return _webApplicationFixture.CreateDefaultClient();
        }

        [Fact]
        public async Task Basic_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/ratingViewComponent/basic");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().Be(@"<div>
    Rating widget goes here - bah
</div>");
        }

    }
}
