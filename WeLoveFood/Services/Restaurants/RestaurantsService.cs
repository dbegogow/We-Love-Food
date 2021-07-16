using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Services.Cities;
using WeLoveFood.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly WeLoveFoodDbContext _data;
        private readonly ICitiesService _citiesService;

        public RestaurantsService(
            WeLoveFoodDbContext data,
            ICitiesService citiesService)
        {
            this._data = data;
            this._citiesService = citiesService;
        }

        public AllCityRestaurantsQueryModel GetCityRestaurantCards(int cityId, AllCityRestaurantsQueryModel query)
        {
            var restaurantsQuery = this._data
                .Restaurants.AsQueryable();

            restaurantsQuery = restaurantsQuery
                .Where(r => r.CityId == cityId);

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                restaurantsQuery = restaurantsQuery
                    .Where(r => r.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var restaurantsCards = restaurantsQuery
                .Select(r => new RestaurantCardViewModel
                {
                    Name = r.Name,
                    ImgUrl = r.ImgUrl
                });

            var cityName = this._citiesService
                .GetCityName(cityId);

            return new AllCityRestaurantsQueryModel
            {
                RestaurantCardViewModels = restaurantsCards,
                CityName = cityName
            };
        }
    }
}
