using System.Linq;
using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Data.Models;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Users;

using static WeLoveFood.Web.WebConstants;
using static WeLoveFood.Test.Data.UsersTestData;
using static WeLoveFood.Test.Data.RestaurantsTestData;

namespace WeLoveFood.Test.Controllers
{
    public class UsersControllerTest
    {
        private const string AuthorizeRoles = ClientRoleName + ", " + ManagerRoleName;

        [Fact]
        public void GetPersonalDataShouldBeForAuthorizedClientsAndManagersUsers()
            => MyController<UsersController>
                .Instance()
                .Calling(c => c.PersonalData())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AuthorizeRoles));

        [Fact]
        public void GetCartShouldReturnCorrectViewWithValidModelWithPersonalData()
            => MyController<UsersController>
                .Instance(controller => controller
                    .WithUser("UserId", "User")
                    .WithData(GetUser()))
                .Calling(c => c.PersonalData())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<PersonalDataFormModel>()
                    .Passing(model => model.FirstName == "Test first name"));

        [Fact]
        public void PostPersonalDataShouldBeForAuthorizedClientsAndManagersUsersAndShouldHaveRestrictionsForHttpGetOnly()
            => MyController<UsersController>
                .Instance()
                .Calling(c => c.PersonalData(new PersonalDataFormModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AuthorizeRoles)
                    .RestrictingForHttpMethod(HttpMethod.Post));


        [Fact]
        public void PostPersonalDataShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<UsersController>
                .Calling(c => c.PersonalData(new PersonalDataFormModel()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<PersonalDataFormModel>());

        [Theory]
        [InlineData("Test first name", "Test last name", "0884184241", "City 1", "Test address")]
        public void PostPersonalDataShouldRedirectWhenValidModel(
            string firstName,
            string lastName,
            string phoneNumber,
            string city,
            string address)
            => MyController<UsersController>
                .Instance(controller => controller
                    .WithUser("ManagerId", "Manager")
                    .WithData(GetRestaurants()))
                .Calling(c => c.PersonalData(new PersonalDataFormModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    City = city,
                    Address = address
                }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<User>(set => set != null &&
                                           set.FirstOrDefault(u => u.FirstName == "Test first name") != null))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<UsersController>(c => c.PersonalData()));
    }
}
