using System;
using AutoMapper;
using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using System.Collections.Generic;
using WeLoveFood.Services.clients;
using WeLoveFood.Models.Restaurants;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private const string WorkingTimeFormat = @"hh\:mm";

        private readonly WeLoveFoodDbContext _data;
        private readonly IConfigurationProvider _mapper;

        private readonly IClientsService _clients;

        public RestaurantsService(
            WeLoveFoodDbContext data,
            IMapper mapper,
            IClientsService clients)
        {
            this._data = data;
            this._mapper = mapper.ConfigurationProvider;

            _clients = clients;
        }

        public bool AddToFavorite(int restaurantId, string userId)
        {
            var clientHasRestaurant = this._clients
                .HasRestaurantInFavorite(userId, restaurantId);

            var restaurant = this.Restaurant(restaurantId);

            if (clientHasRestaurant || restaurant == null)
            {
                return false;
            }

            var client = this._clients
                .Client(userId);

            client
                .Restaurants
                .Add(restaurant);

            this._data.SaveChanges();

            return true;
        }

        public bool IsRestaurantOpen(int id)
            => this._data
                .Restaurants
                .Where(r => r.Id == id && r.IsApproved)
                .Select(r => IsOpen(r.OpeningTime, r.ClosingTime))
                .FirstOrDefault();

        public bool IsRestaurantExist(int id)
            => this._data
                .Restaurants
                .Any(r => r.Id == id && r.IsApproved);

        public bool IsApproved(int id)
            => this._data
                .Restaurants
                .Any(r => r.Id == id && r.IsApproved);

        public void Approve(int id)
        {
            this._data
                .Restaurants
                .Find(id)
                .IsApproved = true;

            this._data.SaveChanges();
        }

        public decimal DeliveryFee(int id)
            => this._data
                .Restaurants
                .Where(r => r.Id == id && r.IsApproved)
                .Select(r => r.DeliveryFee ?? 0)
                .FirstOrDefault();

        public Restaurant Restaurant(int id)
            => this._data
                .Restaurants
                .FirstOrDefault(r => r.Id == id && r.IsApproved);

        public AllCityRestaurantsCardsQueryServiceModel AllCityRestaurantsCards(
            int cityId,
            string searchTerm,
            int currentPage,
            int restaurantsPerPage)
        {
            var restaurantsQuery = this._data
                .Restaurants
                .Where(r => r.IsApproved && r.CityId == cityId)
                .AsQueryable();

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
                    ImgUrl = r.CardImgUrl,
                    IsOpen = IsOpen(r.OpeningTime, r.ClosingTime),
                    MealsCategories = r.MealsCategories.Select(mc => mc.Name).ToList()
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

        public RestaurantServiceModel RestaurantInfo(int id)
            => this._data
                .Restaurants
                .Where(r => r.Id == id && r.IsApproved)
                .Select(r => new RestaurantServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    MainImgUrl = r.MainImgUrl,
                    DeliveryFee = r.DeliveryFee,
                    OpeningTime = r.OpeningTime.ToString(WorkingTimeFormat),
                    ClosingTime = r.ClosingTime.ToString(WorkingTimeFormat),
                    IsOpen = IsOpen(r.OpeningTime, r.ClosingTime)
                })
                .FirstOrDefault();

        public IEnumerable<RestaurantCardServiceModel> Favorite(string userId)
            => this._data
                .Clients
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.Restaurants)
                .Select(r => new RestaurantCardServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    ImgUrl = r.CardImgUrl,
                    IsOpen = IsOpen(r.OpeningTime, r.ClosingTime),
                    MealsCategories = r.MealsCategories.Select(mc => mc.Name).ToList()
                })
                .ToList();

        public IEnumerable<RestaurantCardServiceModel> Managers(string userId)
            => this._data
                .Managers
                .Where(m => m.UserId == userId)
                .SelectMany(m => m.Restaurants)
                .OrderBy(r => r.IsApproved)
                .ProjectTo<RestaurantCardServiceModel>(this._mapper)
                .ToList();

        public IEnumerable<NewRestaurantCardViewModel> NewRestaurants()
            => this._data
                .Restaurants
                .Where(r => !r.IsApproved)
                .ProjectTo<NewRestaurantCardViewModel>(this._mapper)
                .ToList();

        public static bool IsOpen(TimeSpan openingTime, TimeSpan closingTime)
        {
            var now = DateTime.Now.TimeOfDay;

            return now > openingTime && now < closingTime;
        }
    }
}