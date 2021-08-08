using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Orders;
using Microsoft.AspNetCore.Authorization;

namespace WeLoveFood.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _orders;

        public OrdersController(IOrdersService orders)
            => _orders = orders;

        [Authorize]
        public IActionResult Cart()
        {
            var cartAllPortions = this._orders
                .CartAllPortions(this.User.Id());

            return View(cartAllPortions);
        }

        [Authorize]
        public IActionResult Mine()
            => View();
    }
}
