using AutoMapper;
using WeLoveFood.Models.Users;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Users;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Images;
using Microsoft.AspNetCore.Authorization;
using WeLoveFood.Infrastructure.Extensions;

using static WeLoveFood.WebConstants;
using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Controllers
{
    public class UsersController : Controller
    {
        private const string AuthorizeRoles = ClientRoleName + ", " + ManagerRoleName;
        private const string UsersImagesPath = "img/users";

        private readonly IMapper _mapper;

        private readonly IUsersService _users;
        private readonly ICitiesService _cities;
        private readonly IImagesService _images;

        public UsersController(
            IMapper mapper,
            IUsersService users,
            ICitiesService cities,
            IImagesService images)
        {
            this._mapper = mapper;

            this._users = users;
            this._cities = cities;
            this._images = images;
        }

        [Authorize(Roles = AuthorizeRoles)]
        public IActionResult PersonalData()
        {
            var user = this._users
                .PersonalData(this.User.Id());

            var userForm = this._mapper.Map<PersonalDataFormModel>(user);

            return View(userForm);
        }

        [Authorize(Roles = AuthorizeRoles)]
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
                return View(user);
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

        [HttpPost]
        public IActionResult UploadProfilePicture(PersonalDataFormModel user)
        {

            string uniqueFileName = this._images.UploadImage(user.ProfileImg, UsersImagesPath);

            this._users.UpdateProfileImage(this.User.Id(), uniqueFileName);

            return RedirectToAction(nameof(PersonalData));
        }
    }
}