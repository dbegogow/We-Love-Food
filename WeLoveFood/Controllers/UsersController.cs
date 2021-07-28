using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult PersonalData()
            => View();
    }
}
