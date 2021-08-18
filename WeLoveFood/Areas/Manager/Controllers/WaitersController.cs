using System.Linq;
using System.Threading.Tasks;
using WeLoveFood.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Waiters;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Images;
using WeLoveFood.Services.Waiters;
using WeLoveFood.Services.Managers;
using Microsoft.AspNetCore.Identity;
using WeLoveFood.Infrastructure.Extensions;

using static WeLoveFood.WebConstants;
using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;
using static WeLoveFood.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class WaitersController : ManagerController
    {
        private const int NoCity = 0;
        private const string UsersImagesPath = "img/users";
        private const string DuplicateUserNameErrorCode = "DuplicateUserName";

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly ICitiesService _cities;
        private readonly IImagesService _images;
        private readonly IWaitersService _waiters;
        private readonly IManagersService _managers;

        public WaitersController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ICitiesService cities,
            IImagesService images,
            IWaitersService waiters,
            IManagersService managers)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;

            this._cities = cities;
            this._images = images;
            this._waiters = waiters;
            this._managers = managers;
        }

        public IActionResult All(int id)
        {
            var waiters = this._managers
                .Waiters(User.Id(), id)
                .ToList();

            ViewBag.RestaurantId = id;

            return View(waiters);
        }

        public IActionResult Create()
            => View();

        [HttpPost]
        public async Task<IActionResult> Create(int id, AddWaiterFormModel waiter)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var cityId = this._cities
                .Id(waiter.City);

            if (cityId == NoCity)
            {
                ModelState.AddModelError("#", InvalidCity);
            }

            if (!ModelState.IsValid)
            {
                return View(waiter);
            }

            var user = new User
            {
                UserName = waiter.Email,
                Email = waiter.Email,
            };

            var result = await this._userManager.CreateAsync(user, waiter.Password);

            if (result.Succeeded)
            {
                string uniqueFileName = this._images.UploadImage(waiter.ProfileImg, UsersImagesPath);

                this._waiters.CreateWaiter(
                    User.Id(),
                    user.Id,
                    id,
                    waiter.FirstName,
                    waiter.LastName,
                    waiter.PhoneNumber,
                    cityId,
                    waiter.Address,
                    uniqueFileName);

                await _userManager.AddToRoleAsync(user, WaiterRoleName);

                return RedirectToAction("All", "Waiters", new { area = "Manager", id });
            }

            var hasDuplicateUserNameInvalid = result
                    .Errors
                    .Any(e => e.Code == DuplicateUserNameErrorCode);

            ModelState.AddModelError("#",
                hasDuplicateUserNameInvalid ? AlreadyExistUserWithEmail : InvalidPasswordContent);

            return View(waiter);
        }
    }
}
