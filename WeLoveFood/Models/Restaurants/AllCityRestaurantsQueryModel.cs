using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeLoveFood.Models.Restaurants
{
    public class AllCityRestaurantsQueryModel
    {
        [Display(Name = "Search by restaurant name")]
        public string SearchTerm { get; init; }

        public string CityName { get; init; }

        public IEnumerable<RestaurantCardViewModel> RestaurantCardViewModels { get; init; }
    }
}
