using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;

namespace WeLoveFood.Test.Routing
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
