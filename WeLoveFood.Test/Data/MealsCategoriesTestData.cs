using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

using static WeLoveFood.Test.Data.MealsTestData;

namespace WeLoveFood.Test.Data
{
    public static class MealsCategoriesTestData
    {
        public static IEnumerable<MealsCategory> GetMealsCategories(int start, int count)
            => Enumerable
                .Range(start, count)
                .Select(i => new MealsCategory
                {
                    Id = i,
                    Name = $"Meals category {i}",
                    Meals = GetMeals(i * 10, count + 2).ToList()
                });
    }
}
