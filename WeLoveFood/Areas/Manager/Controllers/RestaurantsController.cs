using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Images;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Managers;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Restaurants;
using WeLoveFood.Infrastructure.Extensions;

using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class RestaurantsController : ManagerController
    {
        private const int NoCity = 0;
        private const string RestaurantsImagesPath = "~/img/restaurants";
        private const string MenuMealsPagePath = "~/Manager/Menus/Meals/{0}";

        private readonly IMapper _mapper;

        private readonly IImagesService _images;
        private readonly ICitiesService _cities;
        private readonly IManagersService _managers;
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(
            IMapper mapper,
            IImagesService images,
            ICitiesService cities,
            IManagersService managers,
            IRestaurantsService restaurants)
        {
            this._mapper = mapper;

            this._images = images;
            this._cities = cities;
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
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var restaurant = this._restaurants
                .InformationForEdit(id);

            if (restaurant == null)
            {
                return BadRequest();
            }

            var restaurantForm = this._mapper.Map<EditRestaurantFormModel>(restaurant);

            return View(restaurantForm);
        }

        [HttpPost]
        public IActionResult Edit(EditRestaurantFormModel restaurant)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), restaurant.Id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var cityId = this._cities
                .CityId(restaurant.CityName);

            if (cityId == NoCity)
            {
                ModelState.AddModelError("#", InvalidCity);
            }

            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            this._restaurants
                .Edit(
                    restaurant.Id,
                    restaurant.Name,
                    restaurant.DeliveryFee,
                    restaurant.OpeningTime,
                    restaurant.ClosingTime,
                    cityId);


            string uniqueFileNameCardImg = this._images.UploadImage(restaurant.CardImg, RestaurantsImagesPath);

            if (uniqueFileNameCardImg != null)
            {
                this._restaurants
                    .EditCardImg(restaurant.Id, uniqueFileNameCardImg);
            }

            string uniqueFileNameMainImg = this._images.UploadImage(restaurant.MainImg, RestaurantsImagesPath);

            if (uniqueFileNameMainImg != null)
            {
                this._restaurants
                    .EditMainImg(restaurant.Id, uniqueFileNameMainImg);
            }

            return LocalRedirect(string.Format(MenuMealsPagePath, restaurant.Id));
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
