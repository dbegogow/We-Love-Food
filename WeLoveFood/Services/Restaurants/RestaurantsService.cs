using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly WeLoveFoodDbContext _data;

        public RestaurantsService(WeLoveFoodDbContext data)
            => this._data = data;

        public AllCityRestaurantsCardsQueryServiceModel AllCityRestaurantsCards(
            int cityId,
            string searchTerm,
            int currentPage,
            int restaurantsPerPage)
        {
            var restaurantsQuery = this._data
                .Restaurants.AsQueryable();

            restaurantsQuery = restaurantsQuery
                .Where(r => r.CityId == cityId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                restaurantsQuery = restaurantsQuery
                    .Where(r => r.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var restaurantsCards = restaurantsQuery
                .OrderBy(c => c.Id)
                .Skip((currentPage - 1) * restaurantsPerPage)
                .Take(restaurantsPerPage)
                .Select(r => new RestaurantCardServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    ImgUrl = r.ImgUrl,
                    OpeningTime = r.OpeningTime,
                    ClosingTime = r.ClosingTime
                })
                .ToList();

            var totalRestaurants = restaurantsQuery.Count();

            return new AllCityRestaurantsCardsQueryServiceModel
            {
                CurrentPage = currentPage,
                RestaurantsPerPage = restaurantsPerPage,
                TotalRestaurants = totalRestaurants,
                RestaurantsCards = restaurantsCards
            };
        }
    }
}