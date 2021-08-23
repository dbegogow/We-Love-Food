using Xunit;
using System;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WeLoveFood.Web.Services.Models.Cities;
using WeLoveFood.Web.Controllers;
using static WeLoveFood.Web.CacheConstants;
using static WeLoveFood.Web.Test.Data.Cities;

namespace WeLoveFood.Web.Test.Controllers
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
