using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Models.Restaurants
{
    public class AllCityRestaurantsCardsQueryModel
    {
        public const int RestaurantsPerPage = 16;

        [Display(Name = "Търси ресторант")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; set; } = 1;

        public int TotalRestaurants { get; set; }

        public string CityName { get; init; }

        public IEnumerable<RestaurantCardServiceModel> RestaurantsCards { get; set; }
    }
}
