using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Models.Cities;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Infrastructure.UploadFiles;

using static WeLoveFood.Web.TempDataConstants;
using static WeLoveFood.Web.Infrastructure.UploadFiles.ExceptionMessages;

namespace WeLoveFood.Web.Areas.Admin.Controllers
{
    public class CitiesController : AdminController
    {
        private readonly ICitiesService _cities;

        public CitiesController(ICitiesService cities)
            => this._cities = cities;

        public IActionResult Add()
            => View();

        [HttpPost]
        public async Task<IActionResult> Add(AddCityFormModel city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            var imgUrl = await Clouding.UploadAsync(city.Img);

            if (imgUrl == null)
            {
                ModelState.AddModelError("#", InvalidImageFileExceptionMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(city);
            }

            this._cities
                .Add(city.Name, imgUrl);

            TempData[SuccessMessageKey] = SuccessfulAddedCityMessage;

            return RedirectToAction("All", "Cities", new { area = "" });
        }
    }
}
