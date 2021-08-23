using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeLoveFood.Services.Models.Restaurants;

using static WeLoveFood.Web.Models.Constants.Restaurants.DisplayNames;

namespace WeLoveFood.Web.Models.Restaurants
{
    public class AllCityRestaurantsCardsQueryModel
    {
        public const int RestaurantsPerPage = 16;

        [Display(Name = FindDisplay)]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; set; } = 1;

        public int TotalRestaurants { get; set; }

        public string CityName { get; set; }

        public IEnumerable<RestaurantCardServiceModel> RestaurantsCards { get; set; }
    }
}