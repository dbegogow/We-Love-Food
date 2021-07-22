using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Meals(int id)
        {
            return View();
        }
    }
}
