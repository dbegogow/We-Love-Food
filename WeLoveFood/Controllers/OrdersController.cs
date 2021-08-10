using WeLoveFood.Models.Carts;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Carts;
using WeLoveFood.Services.Users;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Orders;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Portions;
using WeLoveFood.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;
using static WeLoveFood.Models.Constants.Portions.ExceptionMessages;

namespace WeLoveFood.Controllers
{
    public class OrdersController : Controller
    {
        private const int NoPortions = 0;

        private readonly ICartsService _carts;
        private readonly IUsersService _users;
        private readonly ICitiesService _cities;
        private readonly IOrdersService _orders;
        private readonly IClientsService _clients;
        private readonly IPortionsService _portions;

        public OrdersController(
            ICartsService carts,
            IUsersService users,
            ICitiesService cities,
            IOrdersService orders,
            IClientsService clients,
            IPortionsService portions)
        {
            this._carts = carts;
            this._users = users;
            this._cities = cities;
            this._orders = orders;
            this._clients = clients;
            this._portions = portions;
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

        [Authorize]
        [HttpPost]
        public IActionResult Cart(CartUserPersonalDataFormModel user)
        {
            var clientId = this._clients
                .ClientId(this.User.Id());

            var portionsCount = this._portions
                .PortionsCount(clientId);

            if (portionsCount == NoPortions)
            {
                ModelState.AddModelError(string.Empty, NoAddedPortions);
            }

            var cartRestaurantId = this._carts
                .CartRestaurantId(clientId);

            var restaurantCityName = this._cities
                .CityNameByRestaurantId(cartRestaurantId);

            if (restaurantCityName != user.City)
            {
                ModelState.AddModelError(string.Empty, RestaurantNotFromCity);
            }

            if (!ModelState.IsValid)
            {
                var cartAllPortions = this._carts
                    .CartAllPortions(this.User.Id());

                return View(new CartViewModel
                {
                    CartAllPortions = cartAllPortions,
                    PersonalData = new PersonalDataServiceModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        City = user.City,
                        Address = user.Address
                    }
                });
            }

            this._orders
                .MakeOrder(clientId);

            return RedirectToAction("MadeSuccessfulOrder");
        }

        [Authorize]
        public IActionResult MadeSuccessfulOrder()
            => View();

        [Authorize]
        public IActionResult Mine()
            => View();
    }
}
