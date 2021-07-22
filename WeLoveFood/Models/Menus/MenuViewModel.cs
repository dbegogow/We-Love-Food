using System.Collections.Generic;
using WeLoveFood.Services.Models.Menus;

namespace WeLoveFood.Models.Menus
{
    public class MenuViewModel
    {
        public IEnumerable<string> Categories { get; init; }

        public IEnumerable<MealServiceModel> Meals { get; init; }
    }
}
