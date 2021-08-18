using System.Collections.Generic;
using WeLoveFood.Services.Models.Waiters;

namespace WeLoveFood.Services.Waiters
{
    public interface IWaitersService
    {
        void CreateWaiter(
            string userId,
            string waiterUserId,
            int restaurantId,
            string firstName,
            string lastName,
            string phoneNumber,
            int cityId,
            string address,
            string profileImgUrl);

        int RestaurantId(string userId);
    }
}
