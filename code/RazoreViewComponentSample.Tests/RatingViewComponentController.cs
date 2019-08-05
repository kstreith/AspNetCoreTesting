using Microsoft.AspNetCore.Mvc;

namespace RazorViewComponentSample.Tests
{
    public class RatingViewComponentController : Controller
    {
        public IActionResult Basic()
        {
            return ViewComponent("Rating", new { stars = 5 });
        }
    }
}
