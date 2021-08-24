using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

namespace WeLoveFood.Test.Data
{
    public static class MealsTestData
    {
        public static IEnumerable<Meal> GetMeals()
            => Enumerable
                .Range(1, 3)
                .Select(i => new Meal
                {
                    Id = i,
                    Name = $"Meal {i}"
                });
    }
}
