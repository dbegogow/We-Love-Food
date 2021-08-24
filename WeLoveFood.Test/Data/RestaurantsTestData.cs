using System.Linq;
using WeLoveFood.Data.Models;
using System.Collections.Generic;

using static WeLoveFood.Test.Data.CitiesTestData;
using static WeLoveFood.Test.Data.MealsCategoriesTestData;

namespace WeLoveFood.Test.Data
{
    public static class RestaurantsTestData
    {
        public static IEnumerable<Restaurant> GetRestaurants()
        {
            var managerUser = new User { UserName = "Manager" };
            var manager = new Manager { User = managerUser };

            var clientUser = new User { Id = "ClientId", UserName = "Client" };
            var client = new Client { User = clientUser };

            var cities = GetCities().ToList();

            var firstCity = cities[0];
            var secondCity = cities[1];

            var restaurants = Enumerable
                .Range(1, 30)
                .Select(i => new Restaurant
                {
                    Id = i,
                    City = i <= 22 ? firstCity : secondCity,
                    Name = $"Restaurant {i}",
                    IsApproved = i <= 25,
                    Clients = i <= 3 ? new List<Client> { client } : null,
                    Manager = i >= 20 ? manager : null
                });

            return restaurants;
        }

        public static IEnumerable<Restaurant> GetRestaurantsWithMealsCategoriesAndMeals()
            => Enumerable
                .Range(1, 2)
                .Select(i => new Restaurant
                {
                    Id = i,
                    MealsCategories = GetMealsCategories(i * 10, i + 2).ToList()
                });
    }
}
