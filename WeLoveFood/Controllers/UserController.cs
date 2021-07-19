using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Orders()
            => View();

        public IActionResult PersonalData()
            => View();
    }
}
