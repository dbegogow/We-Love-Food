using WeLoveFood.Data;
using Microsoft.AspNetCore.Http;
using WeLoveFood.Models.Constants.Cities;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Restaurant;
using static WeLoveFood.Models.Constants.Restaurants.DisplayNames;
using static WeLoveFood.Models.Constants.Restaurants.ExceptionMessages;

namespace WeLoveFood.Models.Restaurants
{
    public class EditRestaurantFormModel
    {
        [Required(ErrorMessage = RequiredName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        [Display(Name = NameDisplay)]
        public string Name { get; init; }

        public IFormFile CardImg { get; init; }

        public IFormFile MainImg { get; init; }

        [Range(MinimumDeliveryFee, MaximumDeliveryFee, ErrorMessage = InvalidDeliveryFee)]
        [Display(Name = DeliveryFeeDisplay)]
        public decimal? DeliveryFee { get; init; }

        [Required(ErrorMessage = RequiredOpeningTime)]
        [RegularExpression(WorkingTimeRegularExpression, ErrorMessage = InvalidWorkingTime)]
        [Display(Name = OpeningTimeDisplay)]
        public string OpeningTime { get; init; }

        [Required(ErrorMessage = RequiredClosingTime)]
        [RegularExpression(WorkingTimeRegularExpression, ErrorMessage = InvalidWorkingTime)]
        [Display(Name = ClosingTimeDisplay)]
        public string ClosingTime { get; init; }

        [Required(ErrorMessage = ExceptionMessages.RequiredCityName)]
        [StringLength(DataConstants.City.NameMaxLength, ErrorMessage = ExceptionMessages.InvalidCityName, MinimumLength = DataConstants.City.NameMinLength)]
        [Display(Name = CityNameDisplay)]
        public string CityName { get; init; }
    }
}
