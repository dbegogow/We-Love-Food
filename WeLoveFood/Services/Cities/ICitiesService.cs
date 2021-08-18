using System.Collections.Generic;
using WeLoveFood.Services.Models.Cities;

namespace WeLoveFood.Services.Cities
{
    public interface ICitiesService
    {
        void Add(string name, string imgUrl);

        string Name(int id);

        int Id(string cityName);

        string NameByRestaurantId(int restaurantId);

        IEnumerable<CityCardServiceModel> CardsOrderByRestaurantsCount(int? citiesCount = null);
    }
}