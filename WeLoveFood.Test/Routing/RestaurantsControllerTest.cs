using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Restaurants;

namespace WeLoveFood.Test.Routing
{
    public class RestaurantsControllerTest
    {
        [Fact]
        public void AllShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Restaurants/1")
                .To<RestaurantsController>(c => c.All(1, new AllCityRestaurantsCardsQueryModel()));

        [Fact]
        public void FavoriteShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Favorite")
                .To<RestaurantsController>(c => c.Favorite());
    }
}
