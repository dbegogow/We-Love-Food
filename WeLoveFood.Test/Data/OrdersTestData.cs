using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

using static WeLoveFood.Test.Data.UsersTestData;

namespace WeLoveFood.Test.Data
{
    public static class OrdersTestData
    {
        public static IEnumerable<Order> GetOrders()
        {
            var client = GetClient();

            return Enumerable
                 .Range(1, 10)
                 .Select(i => new Order
                 {
                     Id = i,
                     Client = i <= 3 ? client : null,
                     Portions = new List<Portion>{new()
                     {
                         Id = i,
                         Meal = new Meal{Id = i}
                     }},
                     Restaurant = new Restaurant
                     {
                         Id = i,
                         City = new City { Id = i }
                     }
                 });
        }
    }
}
