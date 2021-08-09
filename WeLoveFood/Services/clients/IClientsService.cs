using WeLoveFood.Data.Models;

namespace WeLoveFood.Services.clients
{
    public interface IClientsService
    {
        void CreateClient(string userId);

        Client Client(string userId);

        string ClientId(string userId);

        bool HasRestaurantInFavorite(string userId, int restaurantId);
    }
}
