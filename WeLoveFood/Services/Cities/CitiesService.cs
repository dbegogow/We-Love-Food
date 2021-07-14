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

        public IEnumerable<CityCardViewModel> GetAllCityCardsOrderByRestaurantsCount()
            => this._data
                .Cities
                .OrderByDescending(c => c.Restaurants.Count())
                .Select(c => new CityCardViewModel
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
