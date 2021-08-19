using AutoMapper;
using System.Linq;
using WeLoveFood.Data;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Cities;

namespace WeLoveFood.Services.Cities
{
    public class CitiesService : ICitiesService
    {
        private readonly WeLoveFoodDbContext _data;
        private readonly IConfigurationProvider _mapper;

        public CitiesService(
            WeLoveFoodDbContext data,
            IMapper mapper)
        {
            this._data = data;
            this._mapper = mapper.ConfigurationProvider;
        }

        public void Add(string name, string imgUrl)
        {
            var newCity = new City
            {
                Name = name,
                ImgUrl = imgUrl
            };

            this._data
                .Cities
                .Add(newCity);

            this._data.SaveChanges();
        }

        public string Name(int id)
            => this._data
                .Cities
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefault();

        public int GetId(string cityName)
            => this._data
                .Cities
                .Where(c => c.Name == cityName)
                .Select(c => c.Id)
                .FirstOrDefault();

        public string NameByRestaurantId(int restaurantId)
            => this._data
                .Restaurants
                .Where(r => r.Id == restaurantId)
                .Select(r => r.City.Name)
                .FirstOrDefault();

        public IEnumerable<CityCardServiceModel> CardsOrderByRestaurantsCount(int? citiesCount)
            => this._data
                .Cities
                .OrderByDescending(c => c.Restaurants.Count())
                .Take(citiesCount ?? this._data.Cities.Count())
                .ProjectTo<CityCardServiceModel>(this._mapper)
                .ToList();
    }
}