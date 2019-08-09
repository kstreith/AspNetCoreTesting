using Microsoft.AspNetCore.Mvc;

namespace RazorViewComponentSample.Tests
{
    public class AchievementSummaryViewComponentController : Controller
    {
        public IActionResult Basic()
        {
            return ViewComponent("AchievementSummary");
        }
    }
}
