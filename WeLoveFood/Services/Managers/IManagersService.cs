using System.Collections.Generic;
using WeLoveFood.Services.Models.Waiters;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Managers
{
    public interface IManagersService
    {
        void Create(string userId);

        bool HasRestaurant(string userId, int restaurantId);

        string Id(string userId);

        IEnumerable<ManagersRestaurantServiceModel> Restaurants(string userId);
        
        IEnumerable<ManagerWaiterServiceModel> Waiters(string userId, int restaurantId);
    }
}
