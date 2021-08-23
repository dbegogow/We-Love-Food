using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Users;
using WeLoveFood.Services.Cities;
using WeLoveFood.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using WeLoveFood.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Infrastructure.UploadFiles;

using static WeLoveFood.Web.WebConstants;
using static WeLoveFood.Web.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Web.Controllers
{
    public class UsersController : Controller
    {
        private const string AuthorizeRoles = ClientRoleName + ", " + ManagerRoleName;

        private readonly IMapper _mapper;

        private readonly IUsersService _users;
        private readonly ICitiesService _cities;

        public UsersController(
            IMapper mapper,
            IUsersService users,
            ICitiesService cities)
        {
            this._mapper = mapper;

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
        public async Task<IActionResult> UploadProfilePicture(PersonalDataFormModel user)
        {

            string profileImg = await Clouding.UploadAsync(user.ProfileImg);

            if (profileImg != null)
            {
                this._users.UpdateProfileImage(User.Id(), profileImg);
            }

            return RedirectToAction("PersonalData");
        }
    }
}