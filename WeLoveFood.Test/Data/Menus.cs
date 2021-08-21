using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

using static WeLoveFood.Test.Data.Restaurants;

namespace WeLoveFood.Test.Data
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
