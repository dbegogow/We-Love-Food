using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
            => View();

        public IActionResult Register()
            => View();

        public IActionResult Logout()
            => Redirect("/");
    }
}
