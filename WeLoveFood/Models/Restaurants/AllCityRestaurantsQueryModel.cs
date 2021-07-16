using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeLoveFood.Models.Restaurants
{
    public class AllCityRestaurantsQueryModel
    {
        public const int RestaurantsPerPage = 16;

        [Display(Name = "Търси ресторант")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; set; } = 1;

        public int TotalRestaurants { get; set; }

        public string CityName { get; init; }

        public IEnumerable<RestaurantCardViewModel> RestaurantCardViewModels { get; init; }
    }
}
