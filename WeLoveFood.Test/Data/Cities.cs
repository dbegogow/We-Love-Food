using System.Linq;
using System.Collections.Generic;
using WeLoveFood.Web.Data.Models;

namespace WeLoveFood.Web.Test.Data
{
    public static class Cities
    {
        public static IEnumerable<City> FiveCities()
            => Enumerable.Range(0, 5).Select(i => new City());
    }
}
