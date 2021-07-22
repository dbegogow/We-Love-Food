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

        public IEnumerable<string> RestaurantMenuCategories(int restaurantId)
            => this._data
                .Menus
                .Where(m => m.Id == m.RestaurantId)
                .Select(m => m.Category)
                .ToList();

        public IEnumerable<MealServiceModel> GetMenuMeals(int menuId)
            => this._data
                .Meals
                .Where(m => m.MenuId == menuId)
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
    }
}
