using System;
using AutoMapper;
using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Menus;
using System.Collections.Generic;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Managers;
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

        private readonly IMenusService _menus;
        private readonly IClientsService _clients;
        private readonly IManagersService _managers;

        public RestaurantsService(
            WeLoveFoodDbContext data,
            IMapper mapper,
            IMenusService menus,
            IClientsService clients,
            IManagersService managers)
        {
            this._data = data;
            this._mapper = mapper.ConfigurationProvider;

            this._menus = menus;
            this._clients = clients;
            this._managers = managers;
        }

        public bool IsOpen(int id)
            => this._data
                .Restaurants
                .Where(r => r.Id == id && !r.IsDeleted && r.IsApproved)
                .Select(r => IsOpen(r.OpeningTime, r.ClosingTime))
                .FirstOrDefault();

        public bool IsExist(int id)
            => this._data
                .Restaurants
                .Any(r => r.Id == id && !r.IsDeleted);

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

        public void Approve(int id)
        {
            var restaurant = this.FindRestaurant(id);

            restaurant.IsApproved = true;

            this._data.SaveChanges();
        }

        public void Archive(int id)
        {
            var restaurant = this.FindRestaurant(id);

            restaurant.IsArchived = true;
            restaurant.IsApproved = false;

            this._data.SaveChanges();
        }

        public void UnArchive(int id)
        {
            var restaurant = this.FindRestaurant(id);

            restaurant.IsArchived = false;

            this._data.SaveChanges();
        }

        public void Add(
            string userId,
            string name,
            string cardImgUrl,
            string mainImgUrl,
            decimal? deliveryFee,
            string openingTime,
            string closingTime,
            int cityId)
        {
            var managerId = this._managers
                .GetId(userId);

            var restaurant = new Restaurant
            {
                ManagerId = managerId,
                Name = name,
                CardImgUrl = cardImgUrl,
                MainImgUrl = mainImgUrl,
                DeliveryFee = deliveryFee,
                OpeningTime = TimeSpan.Parse(openingTime),
                ClosingTime = TimeSpan.Parse(closingTime),
                CityId = cityId
            };

            this._data
                .Restaurants
                .Add(restaurant);

            this._data.SaveChanges();
        }

        public void Edit(
            int id,
            string name,
            decimal? deliveryFee,
            string openingTime,
            string closingTime,
            int cityId)
        {
            var restaurant = this.FindRestaurant(id);

            restaurant.Name = name;
            restaurant.DeliveryFee = deliveryFee;
            restaurant.OpeningTime = TimeSpan.Parse(openingTime);
            restaurant.ClosingTime = TimeSpan.Parse(closingTime);
            restaurant.CityId = cityId;

            this._data.SaveChanges();
        }

        public void EditCardImg(int id, string cardImgUrl)
        {
            var restaurant = this.FindRestaurant(id);

            restaurant.CardImgUrl = cardImgUrl;

            this._data.SaveChanges();
        }

        public void EditMainImg(int id, string mainImgUrl)
        {
            var restaurant = this.FindRestaurant(id);

            restaurant.MainImgUrl = mainImgUrl;

            this._data.SaveChanges();
        }

        public void Delete(int id)
        {
            var restaurant = this.FindRestaurant(id);

            restaurant.IsDeleted = true;

            this._data.SaveChanges();

            var mealsCategoriesIds = this._menus
                .RestaurantMealsCategoriesIds(id);

            foreach (var mealsCategoryId in mealsCategoriesIds)
            {
                this._menus
                    .DeleteMealsCategory(mealsCategoryId);
            }
        }

        public decimal DeliveryFee(int id)
            => this._data
                .Restaurants
                .Where(r => r.Id == id && !r.IsDeleted && r.IsApproved)
                .Select(r => r.DeliveryFee ?? 0)
                .FirstOrDefault();

        public Restaurant Restaurant(int id)
            => this._data
                .Restaurants
                .FirstOrDefault(r => r.Id == id && !r.IsDeleted && r.IsApproved);

        public AllCityRestaurantsCardsQueryServiceModel AllCityRestaurantsCards(
            int cityId,
            string searchTerm,
            int currentPage,
            int restaurantsPerPage)
        {
            var restaurantsQuery = this._data
                .Restaurants
                .Where(r => r.IsApproved && !r.IsDeleted && r.CityId == cityId)
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

        public RestaurantServiceModel Information(int id)
            => this._data
                .Restaurants
                .Where(r => r.Id == id && !r.IsDeleted)
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

        public EditRestaurantServiceModel InformationForEdit(int id)
            => this._data
                .Restaurants
                .Where(r => r.Id == id && !r.IsDeleted)
                .ProjectTo<EditRestaurantServiceModel>(this._mapper)
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

        public IEnumerable<NewRestaurantCardViewModel> NewOnes()
            => this._data
                .Restaurants
                .Where(r => !r.IsApproved && !r.IsDeleted && !r.IsArchived)
                .ProjectTo<NewRestaurantCardViewModel>(this._mapper)
                .ToList();

        public static bool IsOpen(TimeSpan openingTime, TimeSpan closingTime)
        {
            var now = DateTime.Now.TimeOfDay;

            return now > openingTime && now < closingTime;
        }

        private Restaurant FindRestaurant(int id)
            => this._data
                .Restaurants
                .FirstOrDefault(r => r.Id == id && !r.IsDeleted);
    }
}