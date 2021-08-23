using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Services.Managers;
using WeLoveFood.Web.Models.Restaurants;
using WeLoveFood.Web.Services.Restaurants;
using WeLoveFood.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Infrastructure.UploadFiles;

using static WeLoveFood.Web.TempDataConstants;
using static WeLoveFood.Web.Models.Constants.Cities.ExceptionMessages;
using static WeLoveFood.Web.Infrastructure.UploadFiles.ExceptionMessages;

namespace WeLoveFood.Web.Areas.Manager.Controllers
{
    public class RestaurantsController : ManagerController
    {
        private const int NoCity = 0;

        private readonly IMapper _mapper;

        private readonly ICitiesService _cities;
        private readonly IManagersService _managers;
        private readonly IRestaurantsService _restaurants;

        public RestaurantsController(
            IMapper mapper,
            ICitiesService cities,
            IManagersService managers,
            IRestaurantsService restaurants)
        {
            this._mapper = mapper;

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
        public async Task<IActionResult> Add(AddRestaurantFormModel restaurant)
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

            string cardImgUrl = await Clouding.UploadAsync(restaurant.CardImg);
            string mainImgUrl = await Clouding.UploadAsync(restaurant.MainImg);

            if (cardImgUrl == null || mainImgUrl == null)
            {
                ModelState.AddModelError("#", InvalidImageFileExceptionMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            this._restaurants
                .Add(
                    User.Id(),
                    restaurant.Name,
                    cardImgUrl,
                    mainImgUrl,
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
        public async Task<IActionResult> Edit(int id, EditRestaurantFormModel restaurant)
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

            string cardImgUrl = await Clouding.UploadAsync(restaurant.CardImg);

            if (cardImgUrl == null)
            {
                ModelState.AddModelError("#", InvalidImageFileExceptionMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            string mainImgUrl = await Clouding.UploadAsync(restaurant.MainImg);

            if (mainImgUrl == null)
            {
                ModelState.AddModelError("#", InvalidImageFileExceptionMessage);
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

            this._restaurants
                .EditCardImg(id, cardImgUrl);

            this._restaurants
                .EditMainImg(id, mainImgUrl);

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
