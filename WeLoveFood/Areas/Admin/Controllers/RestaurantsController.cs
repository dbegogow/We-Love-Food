using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Areas.Admin.Controllers
{
    public class RestaurantsController : AdminController
    {
        public IActionResult New()
        {
            return View();
        }
    }
}
