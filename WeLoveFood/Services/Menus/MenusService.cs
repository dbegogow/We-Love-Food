using AutoMapper;
using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
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

        public bool IsExistInRestaurant(string name, int restaurantId)
            => this._data
                .MealsCategories
                .Any(mc => mc.Name == name && mc.RestaurantId == restaurantId);

        public string CategoryName(int mealsCategoryId)
            => this._data
                .MealsCategories
                .Where(c => c.Id == mealsCategoryId && !c.IsDeleted)
                .Select(c => c.Name)
                .FirstOrDefault();

        public void DeleteMeal(int mealId)
        {
            var meal = this.FindMeal(mealId);

            meal.IsDeleted = true;

            this._data.SaveChanges();
        }

        public void DeleteMealsCategory(int mealsCategoryId)
        {
            var meals = this.FindMealsByMealsCategory(mealsCategoryId);

            foreach (var meal in meals)
            {
                meal.IsDeleted = true;
            }

            var mealsCategory = this.FindMealsCategory(mealsCategoryId);

            mealsCategory.IsDeleted = true;

            this._data.SaveChanges();
        }

        public void AddMealsCategory(int restaurantId, string name)
        {
            var mealsCategory = new MealsCategory
            {
                RestaurantId = restaurantId,
                Name = name
            };

            this._data
                .MealsCategories
                .Add(mealsCategory);

            this._data.SaveChanges();
        }

        public IEnumerable<int> RestaurantMealsCategoriesIds(int restaurantId)
            => this._data
                .MealsCategories
                .Where(mc => mc.Restaurant.Id == restaurantId && !mc.IsDeleted)
                .Select(mc => mc.Id)
                .ToList();

        public IEnumerable<CategoryServiceModel> RestaurantMealsCategories(int restaurantId)
            => this._data
                .MealsCategories
                .Where(mc => mc.RestaurantId == restaurantId && !mc.IsDeleted)
                .ProjectTo<CategoryServiceModel>(this._mapper)
                .ToList();

        public IEnumerable<MealServiceModel> MealsCategory(int mealsCategoryId)
            => this._data
                .Meals
                .Where(m => m.MealsCategoryId == mealsCategoryId && !m.IsDeleted)
                .ProjectTo<MealServiceModel>(this._mapper)
                .ToList();

        private Meal FindMeal(int id)
            => this._data
                .Meals
                .FirstOrDefault(m => m.Id == id && !m.IsDeleted);

        private MealsCategory FindMealsCategory(int id)
            => this._data
                .MealsCategories
                .FirstOrDefault(mc => mc.Id == id && !mc.IsDeleted);

        private IEnumerable<Meal> FindMealsByMealsCategory(int id)
            => this._data
                .Meals
                .Where(m => m.MealsCategory.Id == id)
                .ToList();
    }
}
