using System.Linq;
using WeLoveFood.Data;
using System.Threading.Tasks;
using WeLoveFood.Data.Models;
using Microsoft.AspNetCore.Http;
using WeLoveFood.Services.Images;
using WeLoveFood.Services.Managers;
using Microsoft.AspNetCore.Identity;

using static WeLoveFood.WebConstants;

namespace WeLoveFood.Services.Waiters
{
    public class WaitersService : IWaitersService
    {
        private const string UsersImagesPath = "img/users";
        private const string DuplicateUserNameErrorCode = "DuplicateUserName";
        private const string InvalidPasswordContentErrorCode = "InvalidPasswordContent";

        private readonly WeLoveFoodDbContext _data;
        private readonly UserManager<User> _userManager;

        private readonly IImagesService _images;
        private readonly IManagersService _managers;

        public WaitersService(
            WeLoveFoodDbContext data,
            UserManager<User> userManager,
            IImagesService images,
            IManagersService managers)
        {
            this._data = data;
            this._userManager = userManager;

            this._images = images;
            this._managers = managers;
        }

        public async Task<string> CreateWaiter(
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
            IFormFile profileImg)
        {
            var user = new User
            {
                UserName = email,
                Email = email
            };

            var result = await this._userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var hasDuplicateUserNameInvalid = result
                    .Errors
                    .Any(e => e.Code == DuplicateUserNameErrorCode);

                return hasDuplicateUserNameInvalid
                    ? DuplicateUserNameErrorCode
                    : InvalidPasswordContentErrorCode;
            }

            string uniqueFileName = this._images.UploadImage(profileImg, UsersImagesPath);

            var managerId = this._managers
                .GetId(managerUserId);

            var waiterUser = await this._data
                .Users
                .FindAsync(user.Id);

            waiterUser.FirstName = firstName;
            waiterUser.LastName = lastName;
            waiterUser.PhoneNumber = phoneNumber;
            waiterUser.CityId = cityId;
            waiterUser.Address = address;
            waiterUser.ProfileImgUrl = uniqueFileName;

            var waiter = new Waiter
            {
                ManagerId = managerId,
                RestaurantId = restaurantId,
                UserId = user.Id
            };

            this._data
                .Waiters
                .Add(waiter);

            await this._data.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user, WaiterRoleName);

            return null;
        }

        public int RestaurantId(string userId)
            => this._data
                .Waiters
                .Where(w => w.UserId == userId)
                .Select(w => w.RestaurantId)
                .FirstOrDefault();
    }
}

