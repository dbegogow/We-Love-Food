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

        public IActionResult Meals(int id, int mealsCategoryId = NoMealsCategoryId)
        {
            var allCategories = this._menus
                .RestaurantMealsCategories(id)
                .ToList();

            mealsCategoryId = mealsCategoryId == NoMealsCategoryId ? allCategories[0].Id : mealsCategoryId;

            var meals = this._menus
                .MealsCategory(mealsCategoryId)
                .ToList();

            var categoryName = this._menus
                .CategoryName(mealsCategoryId);

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
