using System.Collections.Generic;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Managers
{
    public interface IManagersService
    {
        void CreateManager(string userId);

        bool HasRestaurant(string userId, int restaurantId);

        IEnumerable<ManagersRestaurantServiceModel> Restaurants(string userId);
    }
}
