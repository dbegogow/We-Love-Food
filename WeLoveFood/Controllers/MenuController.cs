using WeLoveFood.Models.Menu;
using WeLoveFood.Services.Menu;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menus;
        private readonly IRestaurantsService _restaurants;

        public MenuController(
            IMenuService menus,
            IRestaurantsService restaurants)
        {
            _menus = menus;
            _restaurants = restaurants;
        }

        public IActionResult MenuMeals(int id, int categoryId = 1)
        {
            var allCategories = this._menus
                .RestaurantCategories(id);

            var meals = this._menus
                .GetCategoryMeals(categoryId);

            var categoryName = this._menus
                .CategoryName(categoryId);

            var restaurant = this._restaurants
                .RestaurantInfo(id);

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
