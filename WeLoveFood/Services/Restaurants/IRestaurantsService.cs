using WeLoveFood.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public interface IRestaurantsService
    {
        AllCityRestaurantsViewModel GetCityRestaurantCards(int cityId);
    }
}