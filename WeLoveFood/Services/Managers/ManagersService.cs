using WeLoveFood.Data;
using WeLoveFood.Data.Models;

namespace WeLoveFood.Services.Managers
{
    public class ManagersService : IManagersService
    {
        private readonly WeLoveFoodDbContext _data;

        public ManagersService(WeLoveFoodDbContext data)
            => _data = data;

        public void CreateManager(string userId)
        {
            var manager = new Manager { UserId = userId };

            this._data
                .Managers
                .Add(manager);

            this._data.SaveChanges();
        }
    }
}
