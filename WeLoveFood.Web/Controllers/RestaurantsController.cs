using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Models.Restaurants;
using Microsoft.AspNetCore.Authorization;
using WeLoveFood.Web.Services.Restaurants;
using WeLoveFood.Web.Infrastructure.Extensions;

using static WeLoveFood.Web.WebConstants;

namespace WeLoveFood.Web.Controllers
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
                .Name(id);

            query.CityName = cityName;

            return View(query);
        }

        [Authorize(Roles = ClientRoleName)]
        public IActionResult Favorite()
        {
            var favorite = this._restaurants
                .Favorite(User.Id());

            return View(favorite);
        }
    }
}