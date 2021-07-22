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
                .OrderBy(c => c.Id)
                .Skip((query.CurrentPage - 1) * AllCityRestaurantsQueryModel.RestaurantsPerPage)
                .Take(AllCityRestaurantsQueryModel.RestaurantsPerPage)
                .Select(r => new RestaurantCardViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    ImgUrl = r.ImgUrl,
                    OpeningTime = r.OpeningTime,
                    ClosingTime = r.ClosingTime
                })
                .ToList();

            var cityName = this._citiesService
                .GetCityName(cityId);

            var totalRestaurants = restaurantsQuery.Count();

            return new AllCityRestaurantsQueryModel
            {
                CurrentPage = query.CurrentPage,
                TotalRestaurants = totalRestaurants,
                CityName = cityName,
                RestaurantCardViewModels = restaurantsCards
            };
        }
    }
}
