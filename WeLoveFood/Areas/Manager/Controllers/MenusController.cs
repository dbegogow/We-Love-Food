using System.Linq;
using WeLoveFood.Models.Menus;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Menus;
using System.Collections.Generic;
using WeLoveFood.Services.Managers;
using WeLoveFood.Infrastructure.Extensions;

using static WeLoveFood.Models.Constants.Menus.ExceptionMessages;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class MenusController : ManagerController
    {
        private readonly IMenusService _menus;
        private readonly IManagersService _managers;

        public MenusController(
            IMenusService menus,
            IManagersService managers)
        {
            this._menus = menus;
            this._managers = managers;
        }

        public IActionResult Meals(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var menu = new List<ManagerMenuViewModel>();

            var mealsCategories = this._menus
                .RestaurantMealsCategories(id);

            foreach (var mealsCategory in mealsCategories)
            {
                var meals = this._menus
                    .MealsCategory(mealsCategory.Id)
                    .ToList();

                menu.Add(new ManagerMenuViewModel
                {
                    MealsCategory = mealsCategory,
                    Meals = meals
                });
            }

            ViewBag.RestaurantId = id;

            return View(menu);
        }

        public IActionResult AddMealsCategory()
            => View();

        [HttpPost]
        public IActionResult AddMealsCategory(int id, AddMealsCategoryFormModel mealsCategory)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var isExist = this._menus
                .IsExistInRestaurant(mealsCategory.Name, id);

            if (isExist)
            {
                ModelState.AddModelError("#", MealsCategoryAlreadyExist);
            }

            if (!ModelState.IsValid)
            {
                return View(mealsCategory);
            }

            this._menus
                .AddMealsCategory(id, mealsCategory.Name);

            return RedirectToAction("Meals", "Menus", new { area = "Manager", id });
        }
    }
}
