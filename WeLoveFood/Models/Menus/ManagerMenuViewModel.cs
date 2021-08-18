using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Models.Menus
{
    public class ManagerMenuViewModel
    {
        public CategoryServiceModel MealsCategory { get; init; }

        public IEnumerable<MealServiceModel> Meals { get; init; }
    }
}