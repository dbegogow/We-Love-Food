using WeLoveFood.Data.Models;

namespace WeLoveFood.Services.clients
{
    public interface IClientsService
    {
        void Create(string userId);

        string Id(string userId);

        bool HasRestaurantInFavorite(string userId, int restaurantId);

        Client Client(string userId);

    }
}
