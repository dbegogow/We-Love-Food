using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Controllers;
using Xunit;

namespace WeLoveFood.Web.Test.Routing
{
    public class CitiesControllerTest
    {
        [Fact]
        public void CitiesRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cities")
                .To<CitiesController>(c => c.All());
    }
}
