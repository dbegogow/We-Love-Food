using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Portions;
using WeLoveFood.Web.Web.Models.Orders;

namespace WeLoveFood.Web.Web.Controllers.Api
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