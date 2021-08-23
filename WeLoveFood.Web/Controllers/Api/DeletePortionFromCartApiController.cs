using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Models.Orders;
using WeLoveFood.Services.Portions;
using WeLoveFood.Web.Infrastructure.Extensions;

namespace WeLoveFood.Web.Controllers.Api
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
                .DeleteFromCart(portion.Id, this.User.Id());

            return result ? Ok() : BadRequest();
        }
    }
}