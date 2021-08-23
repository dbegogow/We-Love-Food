using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Web.Web.Models.Constants.Cities.DisplayNames;
using static WeLoveFood.Web.Web.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Web.Web.Models.Cities
{
    public class AddCityFormModel
    {
        [Required(ErrorMessage = RequiredCityName)]
        [Display(Name = NameDisplay)]
        public string Name { get; init; }

        [Required(ErrorMessage = RequiredCityImg)]
        public IFormFile Img { get; init; }
    }
}
