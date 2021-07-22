using System.Collections.Generic;
using WeLoveFood.Services.Models.Cities;

namespace WeLoveFood.Services.Cities
{
    public interface ICitiesService
    {
        IEnumerable<CityCardServiceModel> GetAllCitiesCardsOrderByRestaurantsCount();

        string GetCityName(int id);
    }
}