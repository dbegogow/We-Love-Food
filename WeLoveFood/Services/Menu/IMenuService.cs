using System.Collections.Generic;
using WeLoveFood.Services.Models.Menu;

namespace WeLoveFood.Services.Menu
{
    public interface IMenuService
    {
        IEnumerable<CategoryServiceModel> RestaurantCategories(int restaurantId);

        IEnumerable<MealServiceModel> GetCategoryMeals(int categoryId);

        string CategoryName(int categoryId);
    }
}
