using System.Collections.Generic;
using WeLoveFood.Services.Models.Cities;

namespace WeLoveFood.Services.Cities
{
    public interface ICitiesService
    {
        IEnumerable<CityCardServiceModel> GetCitiesCardsOrderByRestaurantsCount(int? citiesCount = null);

        string GetCityName(int id);

        int CityId(string cityName);
    }
}