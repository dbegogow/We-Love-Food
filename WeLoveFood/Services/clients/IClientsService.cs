using WeLoveFood.Data.Models;

namespace WeLoveFood.Services.clients
{
    public interface IClientsService
    {
        void CreateClient(string userId);

        Client GetClient(string userId);
    }
}
