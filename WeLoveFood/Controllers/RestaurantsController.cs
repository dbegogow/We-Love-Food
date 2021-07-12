using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Controllers
{
    public class RestaurantsController : Controller
    {
        public IActionResult All()
            => View();
    }
}
