using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Services.Menus
{
    public interface IMenusService
    {
        string CategoryName(int mealsCategoryId);

        void DeleteMeal(int mealId);

        void DeleteMealsCategory(int mealsCategoryId);

        IEnumerable<int> RestaurantMealsCategoriesIds(int restaurantId);

        IEnumerable<CategoryServiceModel> RestaurantMealsCategories(int restaurantId);

        IEnumerable<MealServiceModel> MealsCategory(int mealsCategoryId);
    }
}
