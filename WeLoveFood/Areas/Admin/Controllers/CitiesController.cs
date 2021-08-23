using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Models.Cities;
using WeLoveFood.Services.Cities;
using WeLoveFood.Infrastructure.UploadFiles;

using static WeLoveFood.TempDataConstants;

namespace WeLoveFood.Areas.Admin.Controllers
{
    public class CitiesController : AdminController
    {
        private const string CitiesImagesPath = "img/cities";

        private readonly IImages _images;
        private readonly ICitiesService _cities;

        public CitiesController(
            IImages images,
            ICitiesService cities)
        {
            this._images = images;
            this._cities = cities;
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

            string uniqueFileName = this._images.Upload(city.Img, CitiesImagesPath);

            this._cities
                .Add(city.Name, uniqueFileName);

            TempData[SuccessMessageKey] = SuccessfulAddedCityMessage;

            return RedirectToAction("All", "Cities", new { area = "" });
        }
    }
}
