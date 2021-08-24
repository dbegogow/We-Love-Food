using Xunit;
using MyTested.AspNetCore.Mvc;

using WeLoveFood.Web.Areas.Admin.Controllers;

namespace WeLoveFood.Test.Routing.Admin
{
    public class CitiesControllerTest
    {
        [Fact]
        public void GetAddShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cities/Add")
                .To<CitiesController>(c => c.Add());

        [Theory]
        [InlineData("Test city")]
        public void PostAddDataShouldBeRoutedCorrectly(string name)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Cities/Add"));
    }
}
