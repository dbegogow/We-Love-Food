using AutoMapper;
using System.Linq;
using WeLoveFood.Data;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Services.Menus
{
    public class MenusService : IMenusService
    {
        private readonly WeLoveFoodDbContext _data;
        private readonly IConfigurationProvider _mapper;

        public MenusService(
            WeLoveFoodDbContext data,
            IMapper mapper)
        {
            _data = data;
            _mapper = mapper.ConfigurationProvider;
        }

        public string CategoryName(int mealsCategoryId)
            => this._data
                .MealsCategories
                .Where(c => c.Id == mealsCategoryId && !c.IsDeleted)
                .Select(c => c.Name)
                .FirstOrDefault();

        public IEnumerable<CategoryServiceModel> RestaurantCategories(int restaurantId)
            => this._data
                .MealsCategories
                .Where(mc => mc.RestaurantId == restaurantId && !mc.IsDeleted)
                .ProjectTo<CategoryServiceModel>(this._mapper)
                .ToList();

        public IEnumerable<MealServiceModel> CategoryMeals(int mealsCategoryId)
            => this._data
                .Meals
                .Where(m => m.MealsCategoryId == mealsCategoryId && !m.IsDeleted)
                .ProjectTo<MealServiceModel>(this._mapper)
                .ToList();
    }
}
