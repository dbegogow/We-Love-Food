using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

namespace WeLoveFood.Test.Data
{
    public static class PortionsTestData
    {
        public static IEnumerable<Portion> GetPortions(int count)
            => Enumerable
                .Range(1, count)
                .Select(i => new Portion
                {
                    Id = i,
                    Quantity = 1 * i,
                    Meal = new Meal
                    {
                        Id = i,
                        Price = 5,
                        MealsCategory = new MealsCategory
                        {
                            Id = i,
                            Restaurant = new Restaurant
                            {
                                Id = i,
                                DeliveryFee = 2,
                                IsApproved = true,
                                City = new City
                                {
                                    Id = i,
                                    Name = "Test City"
                                }
                            }
                        }
                    }
                });
    }
}
