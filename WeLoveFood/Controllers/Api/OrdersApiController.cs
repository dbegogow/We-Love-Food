using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Orders;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Orders;

using static WeLoveFood.WebConstants;

namespace WeLoveFood.Controllers.Api
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrdersService _orders;

        public OrdersApiController(IOrdersService orders)
            => _orders = orders;

        [HttpPost]
        public IActionResult AddMealToCartApiModel(AddMealToCartApiModel meal)
        {
            var result = this._orders
                .AddMealToCart(meal.Id, meal.RestaurantId, this.User.Id());

            return result ? Ok() : BadRequest();
        }
    }
}
