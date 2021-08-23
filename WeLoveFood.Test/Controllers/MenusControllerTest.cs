using System.Linq;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Controllers;
using WeLoveFood.Web.Web.Models.Menus;
using Xunit;

using static WeLoveFood.Web.Test.Data.Menus;
using static WeLoveFood.Web.Test.Data.Restaurants;

namespace WeLoveFood.Web.Test.Controllers
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
