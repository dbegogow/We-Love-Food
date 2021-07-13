using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Models.Cities;
using System.Collections.Generic;

namespace WeLoveFood.Services.Cities
{
    public class CitiesService : ICitiesService
    {
        private readonly WeLoveFoodDbContext _data;

        public CitiesService(WeLoveFoodDbContext data)
            => this._data = data;

        public IEnumerable<CityViewModel> GetAllCities()
            => this._data
                .Cities
                .Select(c => new CityViewModel
                {
                    Name = c.Name,
                    ImgUrl = c.ImgUrl
                })
                .ToList();
    }
}
