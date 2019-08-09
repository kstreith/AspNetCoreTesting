using FluentAssertions;
using RazorViewComponentSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RazorViewComponentSample.Tests
{
    public class AchievementSummaryViewComponentTest : IClassFixture<TestWebServerFixture>
    {
        private readonly TestWebServerFixture _webApplicationFixture;
        public AchievementSummaryViewComponentTest(TestWebServerFixture webApplicationFixture)
        {
            _webApplicationFixture = webApplicationFixture;
        }

        private HttpClient Arrange(List<Achievement> achievements = null)
        {
            achievements = achievements ?? new List<Achievement>();
            _webApplicationFixture.MockAchievementRepository.MockAchievements = achievements;
            return _webApplicationFixture.CreateDefaultClient();
        }

        [Fact]
        public async Task ZeroAchievements_Works()
        {
            // Arrange
            var client = Arrange();

            // Act
            var result = await client.GetAsync("/AchievementSummaryViewComponent/basic");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            responseContent.Should().Be(@"<div>
    No Achievements, start earning now for rewards.
</div>");
        }

        [Fact]
        public async Task TwoAchievements_Works()
        {
            // Arrange
            var achievements = new List<Achievement>
            {
                new Achievement
                {
                    Name = "10 Blog Posts",
                    DateEarned = new DateTimeOffset(2018, 3, 12, 8, 22, 33, TimeSpan.Zero)
                },
                new Achievement
                {
                    Name = "20 Delivered Projects",
                    DateEarned = new DateTimeOffset(2019, 5, 14, 13, 47, 27, TimeSpan.Zero)
                },
            };
            var client = Arrange(achievements);

            // Act
            var result = await client.GetAsync("/AchievementSummaryViewComponent/basic");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            responseContent.Should().Be(@"<div>
    <ul>
            <li>20 Delivered Projects - Tuesday, May 14, 2019</li>
            <li>10 Blog Posts - Monday, March 12, 2018</li>
    </ul>
</div>");
        }

        [Fact]
        public async Task OneHundredAchievements_Works()
        {
            // Arrange
            var achievements = new List<Achievement>();
            var earnedDate = new DateTimeOffset(2018, 3, 12, 8, 22, 33, TimeSpan.Zero);
            foreach (var index in Enumerable.Range(0, 100))
            {
                achievements.Add(new Achievement
                {
                    Name = $"Achievement{index}",
                    DateEarned = earnedDate
                });
            };
            var client = Arrange(achievements);

            // Act
            var result = await client.GetAsync("/AchievementSummaryViewComponent/basic");

            // Assert
            var responseContent = await result.Content.ReadAsStringAsync();
            responseContent.Should().Be(@"<div>
    <div>100 Achievements Earned</div>
    <div>Most recently earned on Monday, March 12, 2018</div>
</div>");
        }

    }
}
