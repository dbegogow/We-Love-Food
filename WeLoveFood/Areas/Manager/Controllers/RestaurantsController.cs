using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Restaurants;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class RestaurantsController : ManagerController
    {
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(IRestaurantsService restaurants)
            => _restaurants = restaurants;

        public IActionResult Mine()
        {
            var restaurantsCards = this._restaurants
                .Managers(this.User.Id());

            return View(restaurantsCards);
        }
    }
}
