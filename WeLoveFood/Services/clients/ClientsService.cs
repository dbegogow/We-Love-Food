using WeLoveFood.Data;
using WeLoveFood.Data.Models;

namespace WeLoveFood.Services.clients
{
    public class ClientsService : IClientsService
    {
        private readonly WeLoveFoodDbContext _data;

        public ClientsService(WeLoveFoodDbContext data)
            => _data = data;

        public void CreateClient(string userId)
        {
            var client = new Client { UserId = userId };

            this._data
                .Clients
                .Add(client);

            this._data.SaveChanges();
        }
    }
}
