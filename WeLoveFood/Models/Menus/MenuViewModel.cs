using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Models.Menus
{
    public class MenuViewModel
    {
        public RestaurantServiceModel Restaurant { get; init; }

        public IEnumerable<CategoryServiceModel> AllCategories { get; init; }

        public string CategoryName { get; init; }

        public IEnumerable<MealServiceModel> Meals { get; init; }
    }
}
