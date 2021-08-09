using System.Linq;
using WeLoveFood.Data;
using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Services.Menus
{
    public class MenusService : IMenusService
    {
        private readonly WeLoveFoodDbContext _data;

        public MenusService(WeLoveFoodDbContext data)
            => _data = data;

        public IEnumerable<CategoryServiceModel> RestaurantCategories(int restaurantId)
            => this._data
                .MealsCategories
                .Where(m => m.RestaurantId == restaurantId)
                .Select(m => new CategoryServiceModel
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToList();

        public IEnumerable<MealServiceModel> CategoryMeals(int mealsCategoryId)
            => this._data
                .Meals
                .Where(m => m.MealsCategoryId == mealsCategoryId)
                .Select(m => new MealServiceModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Weight = m.Weight,
                    Description = m.Description,
                    ImgUrl = m.ImgUrl,
                    Price = m.Price
                })
                .ToList();

        public string CategoryName(int mealsCategoryId)
            => this._data
                .MealsCategories
                .Where(c => c.Id == mealsCategoryId)
                .Select(c => c.Name)
                .FirstOrDefault();
    }
}
