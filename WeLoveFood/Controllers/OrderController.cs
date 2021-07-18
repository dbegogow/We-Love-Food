using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Cart()
            => View();
    }
}
