using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Services.Users;
using WeLoveFood.Web.Web.Infrastructure.UploadFiles;
using WeLoveFood.Web.Web.Models.Users;
using static WeLoveFood.Web.WebConstants;
using static WeLoveFood.Web.Web.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Web.Web.Controllers
{
    public class UsersController : Controller
    {
        private const string AuthorizeRoles = ClientRoleName + ", " + ManagerRoleName;
        private const string UsersImagesPath = "img/users";

        private readonly IMapper _mapper;

        private readonly IImages _images;
        private readonly IUsersService _users;
        private readonly ICitiesService _cities;

        public UsersController(
            IMapper mapper,
            IImages images,
            IUsersService users,
            ICitiesService cities)
        {
            this._mapper = mapper;

            this._images = images;
            this._users = users;
            this._cities = cities;
        }

        [Authorize(Roles = AuthorizeRoles)]
        public IActionResult PersonalData()
        {
            var user = this._users
                .PersonalData(User.Id());

            var userForm = this._mapper.Map<PersonalDataFormModel>(user);

            return View(userForm);
        }

        [Authorize(Roles = AuthorizeRoles)]
        [HttpPost]
        public IActionResult PersonalData(PersonalDataFormModel user)
        {
            var cityId = this._cities
                .GetId(user.City);

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
                    User.Id(),
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    cityId,
                    user.Address);

            return RedirectToAction("PersonalData");
        }

        [HttpPost]
        public IActionResult UploadProfilePicture(PersonalDataFormModel user)
        {

            string uniqueFileName = this._images.Upload(user.ProfileImg, UsersImagesPath);

            this._users.UpdateProfileImage(User.Id(), uniqueFileName);

            return RedirectToAction("PersonalData");
        }
    }
}