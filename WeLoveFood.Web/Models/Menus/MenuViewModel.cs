using System.Collections.Generic;
using WeLoveFood.Web.Services.Models.Menus;
using WeLoveFood.Web.Services.Models.Restaurants;

namespace WeLoveFood.Web.Models.Menus
{
    public class MenuViewModel
    {
        public RestaurantServiceModel Restaurant { get; init; }

        public IEnumerable<CategoryServiceModel> AllCategories { get; init; }

        public string CategoryName { get; init; }

        public IEnumerable<MealServiceModel> Meals { get; init; }
    }
}
