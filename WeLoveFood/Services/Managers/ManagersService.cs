using AutoMapper;
using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Managers
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

        public void CreateManager(string userId)
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

        public string ManagerId(string userId)
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
    }
}
