using WeLoveFood.Models.Users;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Users;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _users;
        private readonly ICitiesService _cities;

        public UsersController(
            IUsersService users,
            ICitiesService cities)
        {
            this._users = users;
            this._cities = cities;
        }

        [Authorize]
        public IActionResult PersonalData()
        {
            var user = this._users
                .PersonalData(this.User.Id());

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PersonalData(PersonalDataFormModel user)
        {
            var cityId = this._cities
                .CityId(user.City);

            if (cityId == 0)
            {
                ModelState.AddModelError(string.Empty, InvalidCity);
            }

            if (!ModelState.IsValid)
            {
                return View(new PersonalDataServiceModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    City = user.City,
                    Address = user.Address
                });
            }

            this._users
                .ChangePersonalData(
                    this.User.Id(),
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    cityId,
                    user.Address);

            return RedirectToAction(nameof(PersonalData));
        }
    }
}
