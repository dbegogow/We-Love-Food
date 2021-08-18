using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Orders;
using WeLoveFood.Services.Portions;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Controllers.Api
{
    [Route("api/removePortion")]
    [ApiController]
    public class RemovePortionApiController : ControllerBase
    {
        private readonly IPortionsService _portions;

        public RemovePortionApiController(IPortionsService portions)
            => this._portions = portions;


        [HttpPost]
        public IActionResult Remove(PortionApiModel portion)
        {
            var quantity = this._portions
                .Remove(portion.Id, this.User.Id());

            return quantity > -1 ? Ok(quantity) : BadRequest();
        }
    }
}