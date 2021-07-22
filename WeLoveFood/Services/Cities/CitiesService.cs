using System.Linq;
using WeLoveFood.Data;
using System.Collections.Generic;
using WeLoveFood.Services.Models.Cities;

namespace WeLoveFood.Services.Cities
{
    public class CitiesService : ICitiesService
    {
        private readonly WeLoveFoodDbContext _data;

        public CitiesService(WeLoveFoodDbContext data)
            => this._data = data;

        public IEnumerable<CityCardServiceModel> GetAllCitiesCardsOrderByRestaurantsCount()
            => this._data
                .Cities
                .OrderByDescending(c => c.Restaurants.Count())
                .Select(c => new CityCardServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImgUrl = c.ImgUrl
                })
                .ToList();

        public string GetCityName(int id)
            => this._data
                .Cities
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefault();
    }
}