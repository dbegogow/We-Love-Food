using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(IRestaurantsService restaurants)
            => this._restaurants = restaurants;

        public IActionResult All(int id, [FromQuery] AllCityRestaurantsCardsQueryModel query)
        {
            var queryResult  = this._restaurants
                .AllCityRestaurantsCards(
                    id,
                    query.SearchTerm,
                    query.CurrentPage,
                    AllCityRestaurantsCardsQueryModel.RestaurantsPerPage);

            query.TotalRestaurants = queryResult.TotalRestaurants;
            query.RestaurantsCards = queryResult.RestaurantsCards;

            return View(query);
        }

        public IActionResult Restaurant(int id)
        {
            return View();
        }
    }
}
