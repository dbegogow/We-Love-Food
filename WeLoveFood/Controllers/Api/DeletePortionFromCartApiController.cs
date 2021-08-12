using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Orders;
using WeLoveFood.Services.Portions;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Controllers.Api
{
    [Route("api/deletePortionFromCart")]
    [ApiController]
    public class DeletePortionFromCartApiController : ControllerBase
    {
        private readonly IPortionsService _portions;

        public DeletePortionFromCartApiController(IPortionsService portions)
            => _portions = portions;

        [HttpPost]
        public IActionResult Delete(PortionApiModel portion)
        {
            var result = this._portions
                .DeletePortionFromCart(portion.Id, this.User.Id());

            return result ? Ok() : BadRequest();
        }
    }
}