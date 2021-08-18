using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Services.Menus
{
    public interface IMenusService
    {
        bool IsMealsCategoryExistInRestaurant(string name, int restaurantId);

        bool IsMealExistInMealsCategory(int mealId, int mealsCategoryId);

        string CategoryName(int mealsCategoryId);

        void DeleteMeal(int mealId);

        void DeleteMealsCategory(int mealsCategoryId);

        void AddMealsCategory(int restaurantId, string name);

        void EditMealsCategory(
            int restaurantId, 
            int mealsCategoryId,
            string name);

        IEnumerable<int> RestaurantMealsCategoriesIds(int restaurantId);

        IEnumerable<CategoryServiceModel> RestaurantMealsCategories(int restaurantId);

        IEnumerable<MealServiceModel> MealsCategory(int mealsCategoryId);
    }
}
