using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Orders;
using WeLoveFood.Services.Portions;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Controllers.Api
{
    [Route("api/addPortion")]
    [ApiController]
    public class AddPortionApiController : ControllerBase
    {
        private readonly IPortionsService _portions;

        public AddPortionApiController(IPortionsService portions)
            => this._portions = portions;


        [HttpPost]
        public IActionResult Add(PortionApiModel portion)
        {
            var quantity = this._portions
                .AddPortion(portion.Id, this.User.Id());

            return quantity > -1 ? Ok(quantity) : BadRequest();
        }
    }
}