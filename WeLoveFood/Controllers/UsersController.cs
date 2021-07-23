using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Orders()
            => View();

        public IActionResult PersonalData()
            => View();
    }
}
