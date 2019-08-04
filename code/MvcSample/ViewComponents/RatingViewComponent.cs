using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSample.ViewComponents
{
    public class RatingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int stars)
        {
            return View();
        }
    }
}
