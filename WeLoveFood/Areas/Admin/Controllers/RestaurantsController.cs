using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Areas.Admin.Controllers
{
    public class RestaurantsController : AdminController
    {
        private const int NoCity = 0;

        private readonly ICitiesService _cities;
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(
            ICitiesService cities,
            IRestaurantsService restaurants)
        {
            this._cities = cities;
            this._restaurants = restaurants;
        }

        public IActionResult New()
        {
            var newRestaurantsCards = this._restaurants
                .NewRestaurants();

            return View(newRestaurantsCards);
        }

        public IActionResult Approve(int id, string cityName)
        {
            var isRestaurantExist = this._restaurants
                .IsRestaurantExist(id);

            if (!isRestaurantExist)
            {
                return BadRequest();
            }

            var isRestaurantApproved = this._restaurants
                .IsApproved(id);

            if (isRestaurantApproved)
            {
                return BadRequest();
            }

            int cityId = this._cities
                .CityId(cityName);

            if (cityId == NoCity)
            {
                return BadRequest();
            }

            this._restaurants
                .Approve(id);

            return RedirectToAction("SuccessfulApproved");
        }

        public IActionResult SuccessfulApproved()
           => View();
    }
}
