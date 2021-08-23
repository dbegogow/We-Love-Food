using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Portions;
using WeLoveFood.Web.Web.Models.Orders;

namespace WeLoveFood.Web.Web.Controllers.Api
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