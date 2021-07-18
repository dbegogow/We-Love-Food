using WeLoveFood.Models.Cities;
using System.Collections.Generic;

namespace WeLoveFood.Services.Cities
{
    public interface ICitiesService
    {
        IEnumerable<CityCardViewModel> GetAllCitiesCardsOrderByRestaurantsCount();

        string GetCityName(int id);
    }
}
