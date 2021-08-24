using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

namespace WeLoveFood.Test.Data
{
    public static class CitiesTestData
    {
        public static IEnumerable<City> GetCities()
            => Enumerable
                .Range(1, 2)
                .Select(i => new City
                {
                    Id = i,
                    Name = $"City {i}"
                });
    }
}
