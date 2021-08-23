using System.Diagnostics;
using WeLoveFood.Web.Models;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Infrastructure.Extensions;

namespace WeLoveFood.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsManager())
            {
                return RedirectToAction("Mine", "Restaurants", new { area = "Manager" });
            }

            if (User.IsWaiter())
            {
                return RedirectToAction("All", "Orders", new { area = "Waiter" });
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
