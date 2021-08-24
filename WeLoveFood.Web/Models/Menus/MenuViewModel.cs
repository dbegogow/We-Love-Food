using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Web.Models.Menus
{
    public class MenuViewModel
    {
        public RestaurantServiceModel Restaurant { get; init; }

        public IEnumerable<MealsCategoryServiceModel> AllMealsCategories { get; init; }

        public string MealsCategoryName { get; init; }

        public IEnumerable<MealServiceModel> Meals { get; init; }
    }
}
