using WeLoveFood.Data.Models;
using System.Collections.Generic;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public interface IRestaurantsService
    {
        bool AddToFavorite(int restaurantId, string userId);

        bool IsRestaurantOpen(int id);

        decimal DeliveryFee(int id);

        Restaurant Restaurant(int id);

        AllCityRestaurantsCardsQueryServiceModel AllCityRestaurantsCards(
            int cityId,
            string searchTerm,
            int currentPage,
            int carsPerPage);

        RestaurantServiceModel RestaurantInfo(int id);

        IEnumerable<RestaurantCardServiceModel> Favorite(string userId);

        IEnumerable<NewRestaurantCardViewModel> NewRestaurants();
    }
}