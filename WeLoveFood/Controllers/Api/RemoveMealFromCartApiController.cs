using WeLoveFood.Models.Carts;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Orders;

namespace WeLoveFood.Controllers.Api
{
    [Route("api/removeMealFromCart")]
    [ApiController]
    public class RemoveMealFromCartApiController : ControllerBase
    {
        private readonly IOrdersService _orders;

        public RemoveMealFromCartApiController(IOrdersService orders)
            => _orders = orders;

        [HttpPost]
        public IActionResult Remove(AddMealToCartApiModel meal)
        {
            var result = this._orders
                .AddMealToCart(meal.Id, meal.RestaurantId, this.User.Id());

            return result ? Ok() : BadRequest();
        }
    }
}