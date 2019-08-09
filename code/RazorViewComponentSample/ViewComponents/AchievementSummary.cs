using Microsoft.AspNetCore.Mvc;
using RazorViewComponentSample.Repository;
using System.Linq;

namespace RazorViewComponentSample.ViewComponents
{
    public class AchievementSummary : ViewComponent
    {
        private readonly IAchievementRepository _repository;

        public AchievementSummary(IAchievementRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var achievements = _repository.GetAllAchievement();
            var achievementCount = achievements.Count();
            if (achievementCount > 10)
            {
                var mostRecentEarned = achievements.OrderByDescending(x => x.DateEarned).First().DateEarned;
                ViewData["MostRecentEarned"] = mostRecentEarned.ToString("D");
                ViewData["AchievementCount"] = achievementCount;
                return View("ShowMoreThan10");
            }
            else if (achievementCount == 0)
            {
                return View("Zero");
            }
            else
            {
                return View("ShowLessThanOrEqualTo10", achievements.ToList());
            }
        }
    }
}
