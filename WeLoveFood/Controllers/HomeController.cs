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
                return RedirectToAction("PersonalData", "Users");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
