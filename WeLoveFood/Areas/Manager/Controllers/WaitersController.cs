using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Waiters;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Waiters;
using WeLoveFood.Services.Managers;
using WeLoveFood.Infrastructure.Extensions;

using static WeLoveFood.TempDataConstants;
using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;
using static WeLoveFood.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class WaitersController : ManagerController
    {
        private const int NoCity = 0;
        private const string DuplicateUserNameErrorCode = "DuplicateUserName";

        private readonly ICitiesService _cities;
        private readonly IWaitersService _waiters;
        private readonly IManagersService _managers;

        public WaitersController(
            ICitiesService cities,
            IWaitersService waiters,
            IManagersService managers)
        {
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

            var resultError = await this._waiters
                .CreateWaiter(
                    User.Id(),
                    id,
                    waiter.Email,
                    waiter.Password,
                    waiter.ConfirmPassword,
                    waiter.FirstName,
                    waiter.LastName,
                    waiter.PhoneNumber,
                    cityId,
                    waiter.Address,
                    waiter.ProfileImg);

            if (resultError == null)
            {
                TempData[SuccessMessageKey] = SuccessfulAddedWaiterMessage;

                return RedirectToAction("All", "Waiters", new { area = "Manager", id });
            }

            ModelState.AddModelError("#",
                resultError == DuplicateUserNameErrorCode
                    ? AlreadyExistUserWithEmail
                    : InvalidPasswordContent);

            return View(waiter);
        }
    }
}
