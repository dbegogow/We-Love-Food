using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WeLoveFood.Services.Waiters
{
    public interface IWaitersService
    {
        Task<string> CreateWaiter(
           string managerUserId,
           int restaurantId,
           string email,
           string password,
           string confirmPassword,
           string firstName,
           string lastName,
           string phoneNumber,
           int cityId,
           string address,
           IFormFile profileImg);

        int RestaurantId(string userId);
    }
}
