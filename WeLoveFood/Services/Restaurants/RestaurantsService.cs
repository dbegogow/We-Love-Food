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

        public AllCityRestaurantsViewModel GetCityRestaurantCards(int cityId)
        {
            var restaurantCards = this._data
                .Restaurants
                .Where(r => r.CityId == cityId)
                .Select(r => new RestaurantCardViewModel
                {
                    Name = r.Name,
                    ImgUrl = r.ImgUrl
                })
                .ToList();

            var cityName = this._citiesService
                .GetCityName(cityId);

            return new AllCityRestaurantsViewModel
            {
                RestaurantCardViewModels = restaurantCards,
                CityName = cityName
            };
        }
    }
}
