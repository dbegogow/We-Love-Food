using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Cities;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Restaurants;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.WebConstants;

namespace WeLoveFood.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ICitiesService _cities;
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(
            ICitiesService cities,
            IRestaurantsService restaurants)
        {
            this._cities = cities;
            this._restaurants = restaurants;
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
                .CityName(id);

            query.CityName = cityName;

            return View(query);
        }

        [Authorize(Roles = ClientRoleName)]
        public IActionResult Favorite()
        {
            var favorite = this._restaurants
                .Favorite(this.User.Id());

            return View(favorite);
        }
    }
}