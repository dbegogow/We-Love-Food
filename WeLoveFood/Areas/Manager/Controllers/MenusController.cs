using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class MenusController : ManagerController
    {
        public IActionResult Meals(int id)
        {
            ViewBag.RestaurantId = id;

            return View();
        }
    }
}
