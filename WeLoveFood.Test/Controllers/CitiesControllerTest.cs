using Xunit;
using System;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Services.Models.Cities;

using static WeLoveFood.Test.Data.Cities;
using static WeLoveFood.Web.CacheConstants;

namespace WeLoveFood.Test.Controllers
{
    public class CitiesControllerTest
    {
        private const int AbsoluteExpirationTime = 30;
        private const int ValidCitiesCount = 5;

        [Fact]
        public void AllShouldReturnCorrectViewWithCities()
            => MyController<CitiesController>
                .Instance(controller => controller
                    .WithData(FiveCities()))
                .Calling(c => c.All())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(AllCitiesCardsCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(AbsoluteExpirationTime))
                        .WithValueOfType<List<CityCardServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<CityCardServiceModel>>()
                    .Passing(model => model.Count == ValidCitiesCount));
    }
}
