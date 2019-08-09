using AuthSample.Tests.MockAuthentication;
using FluentAssertions;
using System.Collections.Generic;
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

        private HttpClient Arrange(string role)
        {
            if (role != null)
            {
                _testServerFixture.MockRoles.Roles = new List<string> { role };
            }
            else
            {
                _testServerFixture.MockRoles.Roles = MockRoles.GetAllRoles();
            }
            return _testServerFixture.CreateDefaultClient();
        }

        [Theory]
        [InlineData("GetValues", true)]
        [InlineData("PostValue", false)]
        public async Task GetValues_Works(string role, bool shouldAuthorize)
        {
            // Arrange
            var client = Arrange(role);

            // Act
            var result = await client.GetAsync("/api/values");

            // Assert
            result.StatusCode.Should().Be(shouldAuthorize ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }

        [Theory]
        [AuthorizedForOnlyThisRole("GetValue")]
        public async Task GetValue_Works(string role, bool shouldAuthorize)
        {
            // Arrange
            var client = Arrange(role);

            // Act
            var result = await client.GetAsync("/api/values/5");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            result.StatusCode.Should().Be(shouldAuthorize ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }

    }
}
