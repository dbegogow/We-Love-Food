using System.Linq;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Controllers;
using WeLoveFood.Models.Menus;
using Xunit;

using static WeLoveFood.Test.Data.Menus;
using static WeLoveFood.Test.Data.Restaurants;

namespace WeLoveFood.Test.Controllers
{
    public class MenusControllerTest
    {
        [Theory]
        [InlineData(1, 1)]
        public void MealsShouldReturnCorrectViewWithValidModel(
            int restaurantId,
            int categoryId)
            => MyController<MenusController>
                .Instance(instance => instance
                    .WithData( Meals()))
                .Calling(c => c.Meals(restaurantId, categoryId))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<MenuViewModel>()
                    .Passing(menu => menu.Meals.Count() == 1));
    }
}
