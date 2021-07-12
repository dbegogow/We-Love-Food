using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Models;
using WeLoveFood.Models.Cities;

namespace WeLoveFood.Controllers
{
    public class HomeController : Controller
    {
        private WeLoveFoodDbContext data;

        public HomeController(WeLoveFoodDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var cities = this.data
                .Cities
                .Select(c => new CityViewModel
                {
                    Name = c.Name,
                    ImgUrl = c.ImgUrl
                })
                .ToList();

            return View(cities);
        }

        public IActionResult Login()
            => View();

        public IActionResult Register()
            => View();

        public IActionResult Logout()
            => Redirect("/");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
