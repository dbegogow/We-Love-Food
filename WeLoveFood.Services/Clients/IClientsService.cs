using WeLoveFood.Web.Data.Models;

namespace WeLoveFood.Web.Services.clients
{
    public interface IClientsService
    {
        void Create(string userId);

        string GetId(string userId);

        bool HasRestaurantInFavorite(string userId, int restaurantId);

        Client Client(string userId);

    }
}
