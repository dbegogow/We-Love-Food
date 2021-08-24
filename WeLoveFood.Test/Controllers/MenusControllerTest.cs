using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;

namespace WeLoveFood.Test.Controllers
{
    public class MenusControllerTest
    {
        [Fact]
        public void MealsShouldReturnCorrectViewWithValidData()
            => MyController<MenusController>
                .Instance(controller => controller
                    .WithData());
    }
}
