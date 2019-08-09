using System.Collections.Generic;
using RazorViewComponentSample.Models;
using RazorViewComponentSample.Repository;

namespace RazorViewComponentSample.Tests
{
    internal class MockAchievementRepository : IAchievementRepository
    {
        public List<Achievement> MockAchievements { get; set; } = new List<Achievement>();

        public IEnumerable<Achievement> GetAllAchievement()
        {
            return MockAchievements;
        }
    }
}