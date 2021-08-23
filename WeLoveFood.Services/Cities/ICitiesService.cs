using System.Collections.Generic;
using WeLoveFood.Web.Services.Models.Cities;

namespace WeLoveFood.Web.Services.Cities
{
    public interface ICitiesService
    {
        void Add(string name, string imgUrl);

        string Name(int id);

        int GetId(string cityName);

        string NameByRestaurantId(int restaurantId);

        IEnumerable<CityCardServiceModel> CardsOrderByRestaurantsCount(int? citiesCount = null);
    }
}