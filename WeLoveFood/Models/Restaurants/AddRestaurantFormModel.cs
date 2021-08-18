using WeLoveFood.Data;
using Microsoft.AspNetCore.Http;
using WeLoveFood.Models.Constants.Cities;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Restaurant;
using static WeLoveFood.Models.Constants.Restaurants.ExceptionMessages;

namespace WeLoveFood.Models.Restaurants
{
    public class AddRestaurantFormModel
    {
        [Required(ErrorMessage = RequiredName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required(ErrorMessage = RequiredCardImg)]
        public IFormFile CardImg { get; init; }

        [Required(ErrorMessage = RequiredMainImg)]
        public IFormFile MainImg { get; init; }

        [Range(MinimumDeliveryFee, MaximumDeliveryFee, ErrorMessage = InvalidDeliveryFee)]
        public decimal? DeliveryFee { get; init; }

        [Required(ErrorMessage = RequiredOpeningTime)]
        [RegularExpression(WorkingTimeRegularExpression, ErrorMessage = InvalidWorkingTime)]
        public string OpeningTime { get; init; }

        [Required(ErrorMessage = RequiredClosingTime)]
        [RegularExpression(WorkingTimeRegularExpression, ErrorMessage = InvalidWorkingTime)]
        public string ClosingTime { get; init; }

        [Required(ErrorMessage = ExceptionMessages.RequiredCityName)]
        [StringLength(DataConstants.City.NameMaxLength, ErrorMessage = ExceptionMessages.InvalidCityName, MinimumLength = DataConstants.City.NameMinLength)]
        public string CityName { get; init; }
    }
}