using WeLoveFood.Models.Menus;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Menus;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers
{
    public class MenusController : Controller
    {
        private readonly IMenusService _menus;
        private readonly IRestaurantsService _restaurants;

        public MenusController(
            IMenusService menus,
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
