using System.Collections.Generic;
using Xunit;
using System.Linq;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Services.Models.Restaurants;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Restaurants;

using static WeLoveFood.Test.Data.TestData;

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
                    .WithData(GetData()))
                .Calling(c => c.All(cityId, new AllCityRestaurantsCardsQueryModel
                {
                    SearchTerm = searchTerm,
                    CurrentPage = page
                }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllCityRestaurantsCardsQueryModel>()
                    .Passing(model => model.TotalRestaurants == totalRestaurantsExpectedCount &&
                                      model.RestaurantsCards.Count() == pageRestaurantsExpectedCount &&
                                      model.SearchTerm == searchTerm &&
                                      model.CurrentPage == page &&
                                      model.CityName == "First city"));

        [Fact]
        public void FavoriteShouldReturnCorrectViewWithClientFavoriteRestaurantsModel()
            => MyController<RestaurantsController>
                .Instance(controller => controller
                    .WithUser("ClientId", "Client")
                    .WithData(GetData()))
                .Calling(c => c.Favorite())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<RestaurantCardServiceModel>>()
                    .Passing(p => p.Count == 3 &&
                                  p.OrderBy(r => r.Id)
                                      .FirstOrDefault()?.Id == 1));
    }
}
