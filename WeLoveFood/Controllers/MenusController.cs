using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class MenusController : Controller
    {
        public IActionResult Meals(int id)
        {
            return View();
        }
    }
}
