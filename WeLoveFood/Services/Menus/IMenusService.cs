using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Services.Menus
{
    public interface IMenusService
    {
        string CategoryName(int mealsCategoryId);

        IEnumerable<CategoryServiceModel> RestaurantCategories(int restaurantId);

        IEnumerable<MealServiceModel> CategoryMeals(int mealsCategoryId);
    }
}
