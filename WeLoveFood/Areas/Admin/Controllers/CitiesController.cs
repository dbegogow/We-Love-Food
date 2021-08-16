using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Areas.Admin.Controllers
{
    public class CitiesController : AdminController
    {
        public IActionResult Add(int restaurantId)
        {
            return View();
        }
    }
}
