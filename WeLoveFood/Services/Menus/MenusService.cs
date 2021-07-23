﻿using System.Linq;
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

        public IEnumerable<string> RestaurantCategories(int restaurantId)
            => this._data
                .Categories
                .Where(m => m.Id == m.RestaurantId)
                .Select(m => m.Name)
                .ToList();

        public IEnumerable<MealServiceModel> GetCategoryMeals(int categoryId)
            => this._data
                .Meals
                .Where(m => m.CategoryId == categoryId)
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
