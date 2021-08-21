using Xunit;
using System;
using WeLoveFood.Controllers;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WeLoveFood.Services.Models.Cities;
using static WeLoveFood.CacheConstants;
using static WeLoveFood.Test.Data.Cities;

namespace WeLoveFood.Test.Controllers
{
    public class CitiesControllerTest
    {
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
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(30))
                        .WithValueOfType<List<CityCardServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<CityCardServiceModel>>()
                    .Passing(model => model.Count == 5));
    }
}
