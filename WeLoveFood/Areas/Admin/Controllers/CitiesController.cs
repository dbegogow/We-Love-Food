using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Cities;
using WeLoveFood.Services.Images;
using WeLoveFood.Services.Cities;

using static WeLoveFood.TempDataConstants;

namespace WeLoveFood.Areas.Admin.Controllers
{
    public class CitiesController : AdminController
    {
        private const string CitiesImagesPath = "img/cities";

        private readonly ICitiesService _cities;
        private readonly IImagesService _images;

        public CitiesController(
            ICitiesService cities,
            IImagesService images)
        {
            this._cities = cities;
            this._images = images;
        }

        public IActionResult Add()
            => View();

        [HttpPost]
        public IActionResult Add(AddCityFormModel city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            string uniqueFileName = this._images.UploadImage(city.Img, CitiesImagesPath);

            this._cities
                .Add(city.Name, uniqueFileName);

            TempData[SuccessMessageKey] = SuccessfulAddedCityMessage;

            return RedirectToAction("All", "Cities", new { area = "" });
        }
    }
}
