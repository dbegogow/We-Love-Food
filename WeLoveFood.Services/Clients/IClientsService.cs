using WeLoveFood.Data.Models;

namespace WeLoveFood.Services.clients
{
    public interface IClientsService
    {
        void Create(string userId);

        string GetId(string userId);

        bool HasRestaurantInFavorite(string userId, int restaurantId);

        Client Client(string userId);

    }
}
