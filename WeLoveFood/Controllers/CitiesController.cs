using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;

namespace WeLoveFood.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICitiesService _cities;

        public CitiesController(ICitiesService cities)
            => this._cities = cities;

        public IActionResult All()
        {
            var citiesCards = this._cities
                .CardsOrderByRestaurantsCount();

            return View(citiesCards);
        }
    }
}
