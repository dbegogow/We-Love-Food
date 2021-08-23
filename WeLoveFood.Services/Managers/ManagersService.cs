using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Web.Data;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Restaurants;
using WeLoveFood.Web.Services.Models.Waiters;

namespace WeLoveFood.Web.Services.Managers
{
    public class ManagersService : IManagersService
    {
        private readonly WeLoveFoodDbContext _data;
        private readonly IConfigurationProvider _mapper;

        public ManagersService(
            WeLoveFoodDbContext data,
            IMapper mapper)
        {
            this._data = data;
            this._mapper = mapper.ConfigurationProvider;
        }

        public void Create(string userId)
        {
            var manager = new Manager { UserId = userId };

            this._data
                .Managers
                .Add(manager);

            this._data.SaveChanges();
        }

        public bool HasRestaurant(string userId, int restaurantId)
            => this._data
                .Managers
                .Any(m => m.UserId == userId && m.Restaurants.Any(r => r.Id == restaurantId && !r.IsDeleted));

        public string GetId(string userId)
            => this._data
                .Managers
                .Where(m => m.UserId == userId)
                .Select(m => m.Id)
                .FirstOrDefault();

        public IEnumerable<ManagersRestaurantServiceModel> Restaurants(string userId)
            => this._data
                .Managers
                .Where(m => m.UserId == userId)
                .SelectMany(m => m.Restaurants)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.IsApproved)
                .ThenBy(r => r.IsArchived)
                .ProjectTo<ManagersRestaurantServiceModel>(this._mapper)
                .ToList();

        public IEnumerable<ManagerWaiterServiceModel> Waiters(string userId, int restaurantId)
            => this._data
                .Managers
                .Where(m => m.UserId == userId)
                .SelectMany(m => m.Waiters)
                .Where(w => w.RestaurantId == restaurantId)
                .ProjectTo<ManagerWaiterServiceModel>(this._mapper)
                .ToList();
    }
}
