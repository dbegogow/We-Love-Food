using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

using static WeLoveFood.Test.Data.MealsTestData;

namespace WeLoveFood.Test.Data
{
    public static class MealsCategoriesTestData
    {
        public static IEnumerable<MealsCategory> GetMealsCategories()
            => Enumerable
                .Range(1, 3)
                .Select(i => new MealsCategory
                {
                    Id = i,
                    Name = $"Meals category {i}",
                    Meals = GetMeals().ToList()
                });
    }
}
