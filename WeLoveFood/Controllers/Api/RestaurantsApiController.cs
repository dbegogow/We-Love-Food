using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Restaurants;
using WeLoveFood.Web.Web.Models.Restaurants;

namespace WeLoveFood.Web.Web.Controllers.Api
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsApiController : ControllerBase
    {
        private readonly IRestaurantsService _restaurants;

        public RestaurantsApiController(IRestaurantsService restaurants)
            => _restaurants = restaurants;

        [HttpPost]
        public IActionResult AddToFavorite(AddRestaurantToFavoriteApiModel restaurant)
        {
            var result = this._restaurants
                .AddToFavorite(restaurant.Id, this.User.Id());

            return result ? Ok() : BadRequest();
        }
    }
}
