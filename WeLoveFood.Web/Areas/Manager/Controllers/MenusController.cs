using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Menus;
using System.Collections.Generic;
using WeLoveFood.Web.Models.Menus;
using WeLoveFood.Services.Managers;
using WeLoveFood.Web.Infrastructure.UploadFiles;
using WeLoveFood.Web.Infrastructure.Extensions;

using static WeLoveFood.Web.TempDataConstants;
using static WeLoveFood.Web.Models.Constants.Menus.ExceptionMessages;
using static WeLoveFood.Web.Infrastructure.UploadFiles.ExceptionMessages;

namespace WeLoveFood.Web.Areas.Manager.Controllers
{
    public class MenusController : ManagerController
    {
        private const int InvalidMealsCategory = 0;

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

        public IActionResult AddMeal()
            => View();

        [HttpPost]
        public async Task<IActionResult> AddMeal(int id, AddMealFormModel meal)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var mealsCategoryId = this._menus
                .MealsCategoryId(meal.MealsCategory, id);

            if (mealsCategoryId == InvalidMealsCategory)
            {
                ModelState.AddModelError("#", MealsCategoryNotExist);
            }

            if (!ModelState.IsValid)
            {
                return View(meal);
            }

            string imgUrl = await Clouding.UploadAsync(meal.Img);

            if (imgUrl == null)
            {
                ModelState.AddModelError("#", InvalidImageFileExceptionMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(meal);
            }

            this._menus
                .AddMeal(
                    meal.Name,
                    meal.Weight,
                    meal.Description,
                    imgUrl,
                    meal.Price,
                    mealsCategoryId);

            TempData[SuccessMessageKey] = SuccessfulAddedMealMessage;

            return RedirectToAction("Meals", "Menus", new { area = "Manager", id });
        }

        public IActionResult AddMealsCategory()
            => View();

        [HttpPost]
        public IActionResult AddMealsCategory(int id, MealsCategoryFormModel mealsCategory)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var isExist = this._menus
                .IsMealsCategoryExistInRestaurant(mealsCategory.Name, id);

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

            TempData[SuccessMessageKey] = SuccessfulAddedMealsCategoryMessage;

            return RedirectToAction("Meals", "Menus", new { area = "Manager", id });
        }

        public IActionResult EditMealsCategory(int id)
        {
            var mealsCategoryName = this._menus
                .CategoryName(id);

            if (mealsCategoryName == null)
            {
                return BadRequest();
            }

            return View(new MealsCategoryFormModel { Name = mealsCategoryName });
        }

        [HttpPost]
        public IActionResult EditMealsCategory(
            int id,
            int mealsCategoryId,
            MealsCategoryFormModel mealsCategory)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var isExist = this._menus
                .IsMealsCategoryExistInRestaurant(mealsCategory.Name, id);

            if (isExist)
            {
                ModelState.AddModelError("#", MealsCategoryAlreadyExist);
            }

            if (!ModelState.IsValid)
            {
                return View(mealsCategory);
            }

            this._menus
                .EditMealsCategory(id, mealsCategoryId, mealsCategory.Name);

            TempData[SuccessMessageKey] = SuccessfulEditedMealsCategoryMessage;

            return RedirectToAction("Meals", "Menus", new { area = "Manager", id });
        }

        public IActionResult DeleteMealsCategory(int id, int mealsCategoryId)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var mealsCategoryName = this._menus
                .CategoryName(mealsCategoryId);

            var isExist = this._menus
                .IsMealsCategoryExistInRestaurant(mealsCategoryName, id);

            if (!isExist)
            {
                return BadRequest();
            }

            this._menus
                .DeleteMealsCategory(mealsCategoryId);

            TempData[SuccessMessageKey] = SuccessfulDeletedMealsCategoryMessage;

            return RedirectToAction("Meals", "Menus", new { area = "Manager", id });
        }

        public IActionResult DeleteMeal(
            int id,
            int mealId,
            int mealsCategoryId)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var isMealExistInMealsCategory = this._menus
                .IsMealExistInMealsCategory(mealId, mealsCategoryId);

            if (!isMealExistInMealsCategory)
            {
                return BadRequest();
            }

            this._menus
                .DeleteMeal(mealId);

            TempData[SuccessMessageKey] = SuccessfulDeletedMealMessage;

            return RedirectToAction("Meals", "Menus", new { area = "Manager", id });
        }
    }
}
