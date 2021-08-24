using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Restaurants;

using static WeLoveFood.Web.TempDataConstants;

namespace WeLoveFood.Web.Areas.Admin.Controllers
{
    public class RestaurantsController : AdminController
    {
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(IRestaurantsService restaurants)
            => this._restaurants = restaurants;

        public IActionResult Newest()
        {
            var newRestaurantsCards = this._restaurants
                .NewOnes();

            return View(newRestaurantsCards);
        }

        public IActionResult Approve(int id)
        {
            var isRestaurantExist = this._restaurants
                .IsExist(id);

            if (!isRestaurantExist)
            {
                return BadRequest();
            }

            this._restaurants
                .Approve(id);

            TempData[SuccessMessageKey] = SuccessfulApprovedRestaurantMessage;

            return RedirectToAction("New", "Restaurants", new { area = "Admin" });
        }
    }
}
