using System.Linq;
using System.Collections.Generic;
using WeLoveFood.Web.Data.Models;
using static WeLoveFood.Web.Test.Data.Restaurants;

namespace WeLoveFood.Web.Test.Data
{
    public static class Menus
    {
        public static IEnumerable<Meal> Meals()
        {
            var restaurants = FiveRestaurants();

            var mealsCategories = new List<MealsCategory>
            {
                new() {RestaurantId = 1},
                new() {RestaurantId = 2}
            };

            var meals = new List<Meal>
            {
                new() {MealsCategoryId = 1},
                new() {MealsCategoryId = 2}
            };

            return meals;
        }
    }
}
