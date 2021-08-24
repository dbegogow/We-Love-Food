using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Models.Cities;
using WeLoveFood.Web.Areas.Admin.Controllers;

namespace WeLoveFood.Test.Controllers.Admin
{
    public class CitiesControllerTest
    {
        [Fact]
        public void GetAddShouldReturnCorrectView()
            => MyController<CitiesController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View();

        [Fact]
        public void PostAddShouldHaveRestrictionsForHttpGetOnly()
            => MyController<CitiesController>
                .Instance()
                .Calling(c => c.Add(new AddCityFormModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void PostAddShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<CitiesController>
                .Calling(c => c.Add(new AddCityFormModel()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddCityFormModel>());
    }
}
