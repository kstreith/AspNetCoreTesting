using Microsoft.AspNetCore.Mvc;

namespace RazorViewComponentSample.ViewComponents
{
    public class RatingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int stars)
        {
            return View();
        }
    }
}
