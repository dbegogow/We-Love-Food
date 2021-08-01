using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WeLoveFood.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Cart()
            => View();

        [Authorize]
        public IActionResult Mine()
            => View();
    }
}
