using System.Collections.Generic;
using WeLoveFood.Web.Services.Models.Menus;

namespace WeLoveFood.Web.Web.Models.Menus
{
    public class ManagerMenuViewModel
    {
        public CategoryServiceModel MealsCategory { get; init; }

        public IEnumerable<MealServiceModel> Meals { get; init; }
    }
}