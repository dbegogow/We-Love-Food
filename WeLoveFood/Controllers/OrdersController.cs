using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WeLoveFood.Controllers
{
    public class OrdersController : Controller
    {
        [Authorize]
        public IActionResult Cart()
        {
            return View();
        }

        [Authorize]
        public IActionResult Mine()
            => View();
    }
}
