using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;

namespace WeLoveFood.Test.Routing
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
