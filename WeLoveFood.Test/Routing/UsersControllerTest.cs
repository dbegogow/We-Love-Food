using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Users;

namespace WeLoveFood.Test.Routing
{
    public class UsersControllerTest
    {
        [Fact]
        public void GetPersonalDataShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/PersonalData")
                .To<UsersController>(c => c.PersonalData());

        [Theory]
        [InlineData("Test first name", "Test last name", "0884184241", "Test City", "Test address")]
        public void PostPersonalDataShouldBeRoutedCorrectly(
            string firstName,
            string lastName,
            string phoneNumber,
            string city,
            string address)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/PersonalData")
                    .WithFormFields(new
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhoneNumber = phoneNumber,
                        City = city,
                        Address = address
                    }))
                .To<UsersController>(c => c.PersonalData(new PersonalDataFormModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    City = city,
                    Address = address
                }))
                .AndAlso()
                .ToValidModelState();

        [Fact]
        public void PostUploadProfilePictureShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Users/UploadProfilePicture"))
                .To<UsersController>(c => c.UploadProfilePicture(new PersonalDataFormModel()));
    }
}
