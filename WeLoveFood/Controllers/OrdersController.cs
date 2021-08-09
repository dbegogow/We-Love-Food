using WeLoveFood.Models.Users;
using WeLoveFood.Models.Carts;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Carts;
using WeLoveFood.Services.Users;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICartsService _carts;
        private readonly IUsersService _users;
        private readonly ICitiesService _cities;

        public OrdersController(
            ICartsService carts,
            IUsersService users, 
            ICitiesService cities)
        {
            this._carts = carts;
            this._users = users;
            this._cities = cities;
        }

        [Authorize]
        public IActionResult Cart()
        {
            var cartAllPortions = this._carts
                .CartAllPortions(this.User.Id());

            var userPersonalData = this._users
                .PersonalData(this.User.Id());

            return View(new CartViewModel
            {
                CartAllPortions = cartAllPortions,
                PersonalData = userPersonalData
            });
        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult Cart(CartUserPersonalDataFormModel user)
        //{
        //    var cityId = this._cities
        //        .CityId(user.City);

        //    if (cityId == 0)
        //    {
        //        ModelState.AddModelError(string.Empty, InvalidCity);
        //    }
        //}

        [Authorize]
        public IActionResult Mine()
            => View();
    }
}
