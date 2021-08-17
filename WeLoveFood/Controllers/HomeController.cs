using WeLoveFood.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsManager())
            {
                return RedirectToAction("Mine", "Restaurants", new { area = "Manager" });
            }
            else if (User.IsClient())
            {
                return RedirectToAction("Mine", "Orders", new { area = "" });
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
