using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
            => this._restaurantsService = restaurantsService;

        public IActionResult All(int id, [FromQuery] AllCityRestaurantsQueryModel query)
        {
            var restaurantsCards = this._restaurantsService
                .GetCityRestaurantCards(id, query);

            return View(restaurantsCards);
        }
    }
}
