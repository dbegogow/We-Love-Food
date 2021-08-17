using System.Linq;
using WeLoveFood.Models.Menus;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Menus;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers
{
    public class MenusController : Controller
    {
        private const int NoCategoryId = 0;

        private readonly IMenusService _menus;
        private readonly IRestaurantsService _restaurants;

        public MenusController(
            IMenusService menus,
            IRestaurantsService restaurants)
        {
            this._menus = menus;
            this._restaurants = restaurants;
        }

        public IActionResult Meals(int id, int categoryId = NoCategoryId)
        {
            var allCategories = this._menus
                .RestaurantMealsCategories(id)
                .ToList();

            categoryId = categoryId == NoCategoryId ? allCategories[0].Id : categoryId;

            var meals = this._menus
                .MealsCategory(categoryId)
                .ToList();

            var categoryName = this._menus
                .CategoryName(categoryId);

            var restaurant = this._restaurants
                .Information(id);

            return View(new MenuViewModel
            {
                AllCategories = allCategories,
                CategoryName = categoryName,
                Meals = meals,
                Restaurant = restaurant
            });
        }
    }
}
