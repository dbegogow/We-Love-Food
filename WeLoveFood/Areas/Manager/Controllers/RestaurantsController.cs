using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Managers;
using WeLoveFood.Services.Restaurants;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class RestaurantsController : ManagerController
    {
        private readonly IManagersService _managers;
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(
            IManagersService managers,
            IRestaurantsService restaurants)
        {
            this._managers = managers;
            this._restaurants = restaurants;
        }

        public IActionResult Mine()
        {
            var restaurants = this._managers
                .Restaurants(this.User.Id());

            return View(restaurants);
        }

        public IActionResult Archive(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(this.User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            this._restaurants
                .Archive(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
