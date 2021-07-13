using WeLoveFood.Models.Cities;
using System.Collections.Generic;

namespace WeLoveFood.Services.Cities
{
    public interface ICitiesService
    {
        IEnumerable<CityViewModel> GetAllCities();
    }
}
