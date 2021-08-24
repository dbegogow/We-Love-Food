using Xunit;
using System.Linq;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Restaurants;
using WeLoveFood.Services.Models.Restaurants;

using static WeLoveFood.Test.Data.RestaurantsTestData;

namespace WeLoveFood.Test.Controllers
{
    public class RestaurantsControllerTest
    {
        [Theory]
        [InlineData(1, null, 1, 16, 22)]
        [InlineData(1, null, 2, 6, 22)]
        [InlineData(1, "Restaurant 11", 1, 1, 1)]
        public void AllShouldReturnCorrectViewWithValidModel(
            int cityId,
            string searchTerm,
            int page,
            int pageRestaurantsExpectedCount,
            int totalRestaurantsExpectedCount)
            => MyController<RestaurantsController>
                .Instance(controller => controller
                    .WithData(GetRestaurants()))
                .Calling(c => c.All(cityId, new AllCityRestaurantsCardsQueryModel
                {
                    SearchTerm = searchTerm,
                    CurrentPage = page
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllCityRestaurantsCardsQueryModel>()
                    .Passing(model => model.TotalRestaurants == totalRestaurantsExpectedCount &&
                                      model.RestaurantsCards.Count() == pageRestaurantsExpectedCount &&
                                      model.SearchTerm == searchTerm &&
                                      model.CurrentPage == page &&
                                      model.CityName == "City 1"));

        [Fact]
        public void FavoriteShouldReturnCorrectViewWithClientFavoriteRestaurantsModel()
            => MyController<RestaurantsController>
                .Instance(controller => controller
                    .WithUser("ClientId", "Client")
                    .WithData(GetRestaurants()))
                .Calling(c => c.Favorite())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<RestaurantCardServiceModel>>()
                    .Passing(p => p.Count == 3 &&
                                  p.OrderBy(r => r.Id)
                                      .FirstOrDefault()?.Id == 1));
    }
}
