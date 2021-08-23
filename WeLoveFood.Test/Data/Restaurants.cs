using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

namespace WeLoveFood.Test.Data
{
    public static class Restaurants
    {
        public static IEnumerable<Restaurant> FiveRestaurants()
            => Enumerable.Range(0, 5).Select(i => new Restaurant());
    }
}