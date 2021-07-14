using WeLoveFood.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;

namespace WeLoveFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService;

        public HomeController(ICitiesService citiesService)
            => _citiesService = citiesService;

        public IActionResult Index()
        {
            var cities = this._citiesService
                .GetAllCityCardsOrderByRestaurantsCount();

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
