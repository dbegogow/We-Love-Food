using System.Linq;
using System.Threading.Tasks;
using WeLoveFood.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Waiters;
using WeLoveFood.Services.Managers;
using WeLoveFood.Web.Models.Waiters;
using Microsoft.AspNetCore.Identity;
using WeLoveFood.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Infrastructure.UploadFiles;

using static WeLoveFood.Web.WebConstants;
using static WeLoveFood.Web.Models.Constants.Cities.ExceptionMessages;
using static WeLoveFood.Web.Infrastructure.UploadFiles.ExceptionMessages;
using static WeLoveFood.Web.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Web.Areas.Manager.Controllers
{
    public class WaitersController : ManagerController
    {
        private const int NoCity = 0;
        private const string DuplicateUserNameErrorCode = "DuplicateUserName";

        private readonly UserManager<User> _userManager;

        private readonly ICitiesService _cities;
        private readonly IWaitersService _waiters;
        private readonly IManagersService _managers;

        public WaitersController(
            UserManager<User> userManager,
            ICitiesService cities,
            IWaitersService waiters,
            IManagersService managers)
        {
            this._userManager = userManager;

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
                Email = waiter.Email
            };

            var result = await this._userManager.CreateAsync(user, waiter.Password);

            if (result.Succeeded)
            {
                string imgUrl = await Clouding.UploadAsync(waiter.ProfileImg);

                if (imgUrl == null)
                {
                    ModelState.AddModelError("#", InvalidImageFileExceptionMessage);
                }

                if (!ModelState.IsValid)
                {
                    return View(waiter);
                }

                this._waiters.CreateWaiter(
                    User.Id(),
                    user.Id,
                    id,
                    waiter.FirstName,
                    waiter.LastName,
                    waiter.PhoneNumber,
                    cityId,
                    waiter.Address,
                    imgUrl);

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