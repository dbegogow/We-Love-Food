using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public interface IRestaurantsService
    {
        AllCityRestaurantsCardsQueryServiceModel AllCityRestaurantsCards(
            int cityId,
            string searchTerm,
            int currentPage,
            int carsPerPage);

        RestaurantServiceModel RestaurantInfo(int id);

        bool AddToFavorite(int restaurantId, string userId);
    }
}