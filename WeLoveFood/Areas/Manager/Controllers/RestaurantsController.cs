using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Managers;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Restaurants;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class RestaurantsController : ManagerController
    {
        private readonly IMapper _mapper;

        private readonly IManagersService _managers;
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(
            IMapper mapper,
            IManagersService managers,
            IRestaurantsService restaurants)
        {
            this._mapper = mapper;

            this._managers = managers;
            this._restaurants = restaurants;
        }

        public IActionResult Mine()
        {
            var restaurants = this._managers
                .Restaurants(this.User.Id());

            return View(restaurants);
        }

        public IActionResult Edit(int id)
        {
            var restaurant = this._restaurants
                .InformationForEdit(id, User.Id());

            if (restaurant == null)
            {
                return BadRequest();
            }

            var restaurantForm = this._mapper.Map<RestaurantFormModel>(restaurant);

            return View(restaurantForm);
        }

        public IActionResult Archive(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(this.User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            this._restaurants
                .Archive(id);

            return RedirectToAction("Mine");
        }

        public IActionResult UnArchive(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(this.User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            this._restaurants
                .UnArchive(id);

            return RedirectToAction("Mine");
        }

        public IActionResult Delete(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(this.User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            this._restaurants
                .Delete(id);

            return RedirectToAction("Mine");
        }
    }
}
