using System.Collections.Generic;
using WeLoveFood.Web.Services.Models.Restaurants;
using WeLoveFood.Web.Services.Models.Waiters;

namespace WeLoveFood.Web.Services.Managers
{
    public interface IManagersService
    {
        void Create(string userId);

        bool HasRestaurant(string userId, int restaurantId);

        string GetId(string userId);

        IEnumerable<ManagersRestaurantServiceModel> Restaurants(string userId);
        
        IEnumerable<ManagerWaiterServiceModel> Waiters(string userId, int restaurantId);
    }
}
