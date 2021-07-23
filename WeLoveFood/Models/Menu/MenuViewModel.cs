using System.Collections.Generic;
using WeLoveFood.Services.Models.Menu;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Models.Menu
{
    public class MenuViewModel
    {
        public RestaurantServiceModel Restaurant { get; init; }

        public IEnumerable<CategoryServiceModel> AllCategories { get; init; }

        public string CategoryName { get; init; }

        public IEnumerable<MealServiceModel> Meals { get; init; }
    }
}
