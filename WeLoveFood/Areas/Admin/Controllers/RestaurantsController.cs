using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Areas.Admin.Controllers
{
    public class RestaurantsController : AdminController
    { 
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(IRestaurantsService restaurants)
            => this._restaurants = restaurants;

        public IActionResult New()
        {
            var newRestaurantsCards = this._restaurants
                .NewRestaurants();

            return View(newRestaurantsCards);
        }

        public IActionResult Approve(int id)
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

            this._restaurants
                .Approve(id);

            return RedirectToAction("SuccessfulApproved");
        }

        public IActionResult SuccessfulApproved()
           => View();
    }
}
