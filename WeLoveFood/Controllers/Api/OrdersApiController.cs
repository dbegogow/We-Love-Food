using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Orders;

namespace WeLoveFood.Controllers.Api
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddMealToCartApiModel(AddMealToCartApiModel meal)
        {
            throw new NotImplementedException();
        }
    }
}
