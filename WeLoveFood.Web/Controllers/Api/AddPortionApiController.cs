using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Portions;
using WeLoveFood.Web.Models.Orders;
using WeLoveFood.Web.Infrastructure.Extensions;

namespace WeLoveFood.Web.Controllers.Api
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
                .Add(portion.Id, this.User.Id());

            return quantity > -1 ? Ok(quantity) : BadRequest();
        }
    }
}