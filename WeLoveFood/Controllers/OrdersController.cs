using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Carts;
using Microsoft.AspNetCore.Authorization;

namespace WeLoveFood.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICartsService _carts;

        public OrdersController(ICartsService carts)
            => this._carts = carts;

        [Authorize]
        public IActionResult Cart()
        {
            var cartAllPortions = this._carts
                .CartAllPortions(this.User.Id());

            return View(cartAllPortions);
        }

        [Authorize]
        public IActionResult Mine()
            => View();
    }
}
