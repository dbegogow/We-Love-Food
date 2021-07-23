using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Menus;
using WeLoveFood.Services.Menus;

namespace WeLoveFood.Controllers
{
    public class MenusController : Controller
    {
        private readonly IMenusService _menus;

        public MenusController(IMenusService menus)
            => _menus = menus;

        public IActionResult MenuMeals(int id, int categoryId = 1)
        {
            var categories = this._menus
                .RestaurantCategories(id);

            var meals = this._menus
                .GetCategoryMeals(categoryId);

            return View(new MenuViewModel
            {
                Categories = categories,
                Meals = meals
            });
        }
    }
}
