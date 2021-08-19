﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Models.Constants.Cities.DisplayNames;
using static WeLoveFood.Models.Constants.Cities.ExceptionMessages;

namespace WeLoveFood.Models.Cities
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
