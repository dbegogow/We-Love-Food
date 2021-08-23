﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Services.Managers;
using WeLoveFood.Web.Services.Restaurants;
using WeLoveFood.Web.Web.Infrastructure.UploadFiles;
using WeLoveFood.Web.Web.Models.Restaurants;
using static WeLoveFood.Web.TempDataConstants;
using static WeLoveFood.Web.Web.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Web.Areas.Manager.Controllers
{
    public class RestaurantsController : ManagerController
    {
        private const int NoCity = 0;
        private const string RestaurantsImagesPath = "img/restaurants";

        private readonly IMapper _mapper;

        private readonly IImages _images;
        private readonly ICitiesService _cities;
        private readonly IManagersService _managers;
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(
            IMapper mapper,
            IImages images,
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

        public IActionResult Add()
            => View();

        [HttpPost]
        public IActionResult Add(AddRestaurantFormModel restaurant)
        {
            var cityId = this._cities
                .GetId(restaurant.CityName);

            if (cityId == NoCity)
            {
                ModelState.AddModelError("#", InvalidCity);
            }

            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            string uniqueFileNameCardImg = this._images.Upload(restaurant.CardImg, RestaurantsImagesPath);
            string uniqueFileNameMainImg = this._images.Upload(restaurant.MainImg, RestaurantsImagesPath);

            this._restaurants
                .Add(
                    User.Id(),
                    restaurant.Name,
                    uniqueFileNameCardImg,
                    uniqueFileNameMainImg,
                    restaurant.DeliveryFee,
                    restaurant.OpeningTime,
                    restaurant.ClosingTime,
                    cityId);

            TempData[SuccessMessageKey] = SuccessfulAddedRestaurantMessage;

            return RedirectToAction("Mine", "Restaurants", new { area = "Manager" });
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
        public IActionResult Edit(int id, EditRestaurantFormModel restaurant)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var cityId = this._cities
                .GetId(restaurant.CityName);

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
                    id,
                    restaurant.Name,
                    restaurant.DeliveryFee,
                    restaurant.OpeningTime,
                    restaurant.ClosingTime,
                    cityId);


            string uniqueFileNameCardImg = this._images.Upload(restaurant.CardImg, RestaurantsImagesPath);

            if (uniqueFileNameCardImg != null)
            {
                this._restaurants
                    .EditCardImg(id, uniqueFileNameCardImg);
            }

            string uniqueFileNameMainImg = this._images.Upload(restaurant.MainImg, RestaurantsImagesPath);

            if (uniqueFileNameMainImg != null)
            {
                this._restaurants
                    .EditMainImg(id, uniqueFileNameMainImg);
            }

            TempData[SuccessMessageKey] = SuccessfulEditedRestaurantMessage;

            return RedirectToAction("Meals", "Menus", new { area = "Manager", id });
        }

        public IActionResult Archive(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            this._restaurants
                .Archive(id);

            TempData[SuccessMessageKey] = SuccessfulArchivedRestaurantMessage;

            return RedirectToAction("Mine");
        }

        public IActionResult UnArchive(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            this._restaurants
                .UnArchive(id);

            TempData[SuccessMessageKey] = SuccessfulUnArchivedRestaurantMessage;

            return RedirectToAction("Mine");
        }

        public IActionResult Delete(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            this._restaurants
                .Delete(id);

            TempData[SuccessMessageKey] = SuccessfulDeleteRestaurantMessage;

            return RedirectToAction("Mine");
        }
    }
}
