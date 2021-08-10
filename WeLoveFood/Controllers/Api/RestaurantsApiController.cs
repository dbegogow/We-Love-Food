using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Controllers.Api
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsApiController : ControllerBase
    {
        private readonly IRestaurantsService _restaurants;

        public RestaurantsApiController(IRestaurantsService restaurants)
            => _restaurants = restaurants;

        [HttpPost]
        public IActionResult AddToFavorite(AddToFavoriteApiModel restaurant)
        {
            var result = this._restaurants
                .AddToFavorite(restaurant.Id, this.User.Id());

            return result ? Ok() : BadRequest();
        }
    }
}
