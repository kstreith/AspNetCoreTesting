using RazorViewComponentSample.Models;
using System.Collections.Generic;

namespace RazorViewComponentSample.Repository
{
    public interface IAchievementRepository
    {
        IEnumerable<Achievement> GetAllAchievement();
    }
}
