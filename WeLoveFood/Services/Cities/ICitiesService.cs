using System.Collections.Generic;
using WeLoveFood.Services.Models.Cities;

namespace WeLoveFood.Services.Cities
{
    public interface ICitiesService
    {
        void AddCity(string name, string imgUrl);

        string CityName(int id);

        int CityId(string cityName);

        string CityNameByRestaurantId(int restaurantId);

        IEnumerable<CityCardServiceModel> CitiesCardsOrderByRestaurantsCount(int? citiesCount = null);
    }
}