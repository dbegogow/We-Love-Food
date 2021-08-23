using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Web.Data;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Menus;

namespace WeLoveFood.Web.Services.Menus
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

        public bool IsMealsCategoryExistInRestaurant(string name, int restaurantId)
            => this._data
                .MealsCategories
                .Any(mc => mc.Name == name && mc.RestaurantId == restaurantId && !mc.IsDeleted);

        public bool IsMealExistInMealsCategory(int mealId, int mealsCategoryId)
            => this._data
                .Meals
                .Any(m => m.Id == mealId && m.MealsCategoryId == mealsCategoryId && !m.IsDeleted);

        public int MealsCategoryId(string mealsCategoryName, int restaurantId)
            => this._data
                .MealsCategories
                .Where(mc => mc.Name == mealsCategoryName && mc.RestaurantId == restaurantId)
                .Select(mc => mc.Id)
                .FirstOrDefault();

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

        public void EditMealsCategory(
            int restaurantId,
            int mealsCategoryId,
            string name)
        {
            var mealsCategory = this.FindMealsCategory(mealsCategoryId);

            mealsCategory.Name = name;

            this._data.SaveChanges();
        }

        public void AddMeal(
            string name,
            int weight,
            string description,
            string imgUrl,
            decimal price,
            int mealsCategoryId)
        {
            var meal = new Meal
            {
                Name = name,
                Weight = weight,
                Description = description,
                ImgUrl = imgUrl,
                Price = price,
                MealsCategoryId = mealsCategoryId
            };

            this._data
                .Meals
                .Add(meal);

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
