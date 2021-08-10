using WeLoveFood.Data.Models;
using System.Collections.Generic;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public interface IRestaurantsService
    {
        bool AddToFavorite(int restaurantId, string userId);

        decimal DeliveryFee(int restaurantId);

        Restaurant Restaurant(int id);

        AllCityRestaurantsCardsQueryServiceModel AllCityRestaurantsCards(
            int cityId,
            string searchTerm,
            int currentPage,
            int carsPerPage);

        RestaurantServiceModel RestaurantInfo(int id);

        IEnumerable<RestaurantCardServiceModel> Favorite(string userId);
    }
}