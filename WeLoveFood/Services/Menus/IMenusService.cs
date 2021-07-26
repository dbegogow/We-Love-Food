using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Services.Menus
{
    public interface IMenusService
    {
        IEnumerable<CategoryServiceModel> RestaurantCategories(int restaurantId);

        IEnumerable<MealServiceModel> GetCategoryMeals(int mealsCategoryId);

        string CategoryName(int mealsCategoryId);
    }
}
