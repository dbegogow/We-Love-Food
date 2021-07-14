using System.Collections.Generic;

namespace WeLoveFood.Models.Restaurants
{
    public class AllCityRestaurantsViewModel
    {
        public string CityName { get; init; }

        public IEnumerable<RestaurantCardViewModel> RestaurantCardViewModels { get; init; }
    }
}
