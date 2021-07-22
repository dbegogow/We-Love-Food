using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Services.Menus
{
    public interface IMenusService
    {
        IEnumerable<string> RestaurantMenuCategories(int restaurantId);

        IEnumerable<MealServiceModel> GetMenuMeals(int menuId);
    }
}
