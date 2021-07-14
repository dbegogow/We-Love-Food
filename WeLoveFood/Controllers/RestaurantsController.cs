using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
            => this._restaurantsService = restaurantsService;

        public IActionResult All(int id)
        {
            var cityRestaurants = this._restaurantsService
                .GetCityRestaurantCards(id);

            return View(cityRestaurants);
        }
    }
}
