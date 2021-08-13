using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Areas.Admin.Controllers
{
    public class RestaurantsController : AdminController
    {
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(IRestaurantsService restaurants)
            => _restaurants = restaurants;

        public IActionResult New()
        {
            var newRestaurantsCards = this._restaurants
                .NewRestaurants();

            return View(newRestaurantsCards);
        }

        public IActionResult Add(int id)
        {
            var isRestaurantApproved = this._restaurants
                .IsApproved(id);

            if (isRestaurantApproved)
            {
                return BadRequest();
            }

            this._restaurants
                .Approve(id);

            return RedirectToAction("New");
        }
    }
}
