using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;

using OrdersController = WeLoveFood.Web.Areas.Waiter.Controllers.OrdersController;
using RestaurantsController = WeLoveFood.Web.Areas.Manager.Controllers.RestaurantsController;

using static WeLoveFood.Web.WebConstants;

namespace WeLoveFood.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWhenUserIsClient()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithUser("Client", new[] { ClientRoleName }))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexShouldReturnViewWenNoUser()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexShouldRedirectToManagerRestaurantsControllerWhenUserIsManager()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithUser("Manager", new[] { ManagerRoleName }))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<RestaurantsController>(c => c.Mine()));

        [Fact]
        public void IndexShouldRedirectToWaiterOrdersControllerWhenUserIsWaiter()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithUser("Waiter", new[] { WaiterRoleName }))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<OrdersController>(c => c.All()));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
