using MyTested.AspNetCore.Mvc;
using WeLoveFood.Controllers;
using Xunit;

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
