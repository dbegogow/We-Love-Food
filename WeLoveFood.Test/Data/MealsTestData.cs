using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

namespace WeLoveFood.Test.Data
{
    public static class MealsTestData
    {
        public static IEnumerable<Meal> GetMeals(int start, int count)
            => Enumerable
                .Range(start, count)
                .Select(i => new Meal
                {
                    Id = i,
                    Name = $"Meal {i}"
                });
    }
}
