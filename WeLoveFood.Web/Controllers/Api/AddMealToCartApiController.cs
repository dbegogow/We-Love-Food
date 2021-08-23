using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Models.Carts;
using WeLoveFood.Web.Services.Orders;
using WeLoveFood.Web.Infrastructure.Extensions;

namespace WeLoveFood.Web.Controllers.Api
{
    [Route("api/addMealToCart")]
    [ApiController]
    public class AddMealToCartApiController : ControllerBase
    {
        private readonly IOrdersService _orders;

        public AddMealToCartApiController(IOrdersService orders)
            => _orders = orders;

        [HttpPost]
        public IActionResult Add(AddMealToCartApiModel meal)
        {
            var result = this._orders
                .AddMealToCart(meal.Id, meal.RestaurantId, this.User.Id());

            return result ? Ok() : BadRequest();
        }
    }
}
