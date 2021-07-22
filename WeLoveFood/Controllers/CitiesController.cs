using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;

namespace WeLoveFood.Controllers
{
    public class Cities : Controller
    {
        private readonly ICitiesService _cities;

        public Cities(ICitiesService cities)
            => this._cities = cities;

        public IActionResult All()
        {
            var citiesCards = this._cities
                .GetAllCitiesCardsOrderByRestaurantsCount();

            return View(citiesCards);
        }
    }
}
