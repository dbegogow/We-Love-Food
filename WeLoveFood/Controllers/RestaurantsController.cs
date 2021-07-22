using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantsService _restaurants;
        private readonly ICitiesService _cities;

        public RestaurantsController(
            IRestaurantsService restaurants,
            ICitiesService cities)
        {
            this._restaurants = restaurants;
            this._cities = cities;
        }

        public IActionResult All(int id, [FromQuery] AllCityRestaurantsCardsQueryModel query)
        {
            var queryResult = this._restaurants
                .AllCityRestaurantsCards(
                    id,
                    query.SearchTerm,
                    query.CurrentPage,
                    AllCityRestaurantsCardsQueryModel.RestaurantsPerPage);

            query.TotalRestaurants = queryResult.TotalRestaurants;
            query.RestaurantsCards = queryResult.RestaurantsCards;

            var cityName = this._cities
                .GetCityName(id);

            query.CityName = cityName;

            return View(query);
        }

        public IActionResult Menu(int id)
        {
            return View();
        }
    }
}