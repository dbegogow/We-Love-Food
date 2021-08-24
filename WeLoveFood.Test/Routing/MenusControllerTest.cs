using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;

namespace WeLoveFood.Test.Routing
{
    public class MenusControllerTest
    {
        [Fact]
        public void MealsShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Menu/1")
                .To<MenusController>(c => c.Meals(1, null));

        [Fact]
        public void MealsShouldBeRoutedCorrectlyWithQueryParameter()
            => MyRouting
                .Configuration()
                .ShouldMap("/Menu/1?mealsCategoryId=1")
                .To<MenusController>(c => c.Meals(1, 1));
    }
}
