using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

namespace WeLoveFood.Test.Data
{
    public static class Cities
    {
        public static IEnumerable<City> FiveCities()
            => Enumerable.Range(0, 5).Select(i => new City());
    }
}
