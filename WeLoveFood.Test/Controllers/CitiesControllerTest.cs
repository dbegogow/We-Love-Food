using Xunit;
using System;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Services.Models.Cities;

using static WeLoveFood.Test.Data.TestData;
using static WeLoveFood.Web.CacheConstants;

namespace WeLoveFood.Test.Controllers
{
    public class CitiesControllerTest
    {
        [Fact]
        public void AllShouldReturnCorrectViewWithValidModel()
            => MyController<CitiesController>
                .Instance(controller => controller
                    .WithData(GetData()))
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
                    .Passing(model => model.Count == 2));
    }
}