using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Services.Managers;
using WeLoveFood.Web.Services.Waiters;
using WeLoveFood.Web.Web.Infrastructure.UploadFiles;
using WeLoveFood.Web.Web.Models.Waiters;
using static WeLoveFood.Web.WebConstants;
using static WeLoveFood.Web.Web.Models.Constants.Cities.ExceptionMessages;
using static WeLoveFood.Web.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Web.Areas.Manager.Controllers
{
    public class WaitersController : ManagerController
    {
        private const int NoCity = 0;
        private const string UsersImagesPath = "img/users";
        private const string DuplicateUserNameErrorCode = "DuplicateUserName";

        private readonly UserManager<User> _userManager;

        private readonly IImages _images;
        private readonly ICitiesService _cities;
        private readonly IWaitersService _waiters;
        private readonly IManagersService _managers;

        public WaitersController(
            UserManager<User> userManager,
            IImages images,
            ICitiesService cities,
            IWaitersService waiters,
            IManagersService managers)
        {
            this._userManager = userManager;

            this._images = images;
            this._cities = cities;
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
                .GetId(waiter.City);

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
                string uniqueFileName = this._images.Upload(waiter.ProfileImg, UsersImagesPath);

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