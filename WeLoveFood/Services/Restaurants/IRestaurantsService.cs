using WeLoveFood.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public interface IRestaurantsService
    {
        AllCityRestaurantsQueryModel GetCityRestaurantCards(int cityId, AllCityRestaurantsQueryModel query);
    }
}