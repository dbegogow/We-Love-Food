using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Managers;

namespace WeLoveFood.Services.Waiters
{
    public class WaitersService : IWaitersService
    {
        private readonly WeLoveFoodDbContext _data;

        private readonly IManagersService _managers;

        public WaitersService(
            WeLoveFoodDbContext data,
            IManagersService managers)
        {
            this._data = data;
            this._managers = managers;
        }

        public void CreateWaiter(
            string userId,
            string waiterUserId,
            int restaurantId,
            string firstName,
            string lastName,
            string phoneNumber,
            int cityId,
            string address,
            string profileImgUrl)
        {
            var managerId = this._managers
                .Id(userId);

            var waiterUser = this._data
                .Users
                .Find(waiterUserId);

            waiterUser.FirstName = firstName;
            waiterUser.LastName = lastName;
            waiterUser.PhoneNumber = phoneNumber;
            waiterUser.CityId = cityId;
            waiterUser.Address = address;
            waiterUser.ProfileImgUrl = profileImgUrl;

            var waiter = new Waiter
            {
                ManagerId = managerId,
                RestaurantId = restaurantId,
                UserId = waiterUserId
            };

            this._data
                .Waiters
                .Add(waiter);

            this._data.SaveChanges();
        }

        public int RestaurantId(string userId)
            => this._data
                .Waiters
                .Where(w => w.UserId == userId)
                .Select(w => w.RestaurantId)
                .FirstOrDefault();
    }
}

