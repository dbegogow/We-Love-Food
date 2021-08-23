using System.Linq;
using System.Collections.Generic;
using WeLoveFood.Web.Data.Models;

namespace WeLoveFood.Web.Test.Data
{
    public static class Restaurants
    {
        public static IEnumerable<Restaurant> FiveRestaurants()
            => Enumerable.Range(0, 5).Select(i => new Restaurant());
    }
}
