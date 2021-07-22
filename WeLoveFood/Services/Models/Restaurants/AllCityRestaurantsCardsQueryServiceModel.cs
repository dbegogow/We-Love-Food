using System.Collections.Generic;

namespace WeLoveFood.Services.Models.Restaurants
{
    public class AllCityRestaurantsCardsQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int RestaurantsPerPage { get; init; }

        public int TotalRestaurants { get; init; }

        public IEnumerable<RestaurantCardServiceModel> RestaurantsCards { get; init; }
    }
}
