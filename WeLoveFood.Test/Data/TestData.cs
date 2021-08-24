using WeLoveFood.Data.Models;
using System.Collections.Generic;
using MyTested.AspNetCore.Mvc;

namespace WeLoveFood.Test.Data
{
    public static class TestData
    {
        public static IEnumerable<Restaurant> GetData()
        {
            var managerUser = new User { UserName = "Manager" };
            var manager = new Manager { User = managerUser };

            var clientUser = new User { Id = "ClientId", UserName = "Client" };
            var client = new Client { User = clientUser };

            var firstCity = new City { Id = 1, Name = "First city" };
            var secondCity = new City { Id = 2, Name = "Second city" };

            var restaurants = new List<Restaurant>();

            for (int i = 1; i <= 30; i++)
            {
                var restaurant = new Restaurant
                {
                    Id = i,
                    City = i <= 22 ? firstCity : secondCity,
                    Name = $"Restaurant {i}",
                    IsApproved = i <= 25,
                    Clients = i <= 3 ? new List<Client> { client } : null
                };


                if (i >= 20)
                {
                    restaurant.Manager = manager;
                }

                restaurants.Add(restaurant);
            }

            return restaurants;
        }
    }
}
