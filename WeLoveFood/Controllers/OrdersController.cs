using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Cart()
            => View();

        public IActionResult Mine()
            => View();
    }
}
