using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Menus;
using WeLoveFood.Web.Models.Menus;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Web.Controllers
{
    public class MenusController : Controller
    {
        private const int NoMealsCategoryId = 0;

        private readonly IMenusService _menus;
        private readonly IRestaurantsService _restaurants;

        public MenusController(
            IMenusService menus,
            IRestaurantsService restaurants)
        {
            this._menus = menus;
            this._restaurants = restaurants;
        }

        public IActionResult Meals(int id, int? mealsCategoryId)
        {
            var allCategories = this._menus
                .RestaurantMealsCategories(id)
                .ToList();

            var firstMealsCategoryId = allCategories
                .Any()
                ? allCategories[0].Id
                : NoMealsCategoryId;

            mealsCategoryId ??= firstMealsCategoryId;

            var meals = this._menus
                .MealsCategory((int)mealsCategoryId)
                .ToList();

            var categoryName = this._menus
                .CategoryName((int)mealsCategoryId);

            var restaurant = this._restaurants
                .Information(id);

            return View(new MenuViewModel
            {
                AllMealsCategories = allCategories,
                MealsCategoryName = categoryName,
                Meals = meals,
                Restaurant = restaurant
            });
        }
    }
}
