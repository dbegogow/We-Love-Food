using Xunit;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Carts;

namespace WeLoveFood.Test.Routing
{
    public class OrdersControllerTest
    {
        [Fact]
        public void GetCartShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cart")
                .To<OrdersController>(c => c.Cart());

        [Theory]
        [InlineData("Test first name", "Test last name", "0884184241", "Test City", "Test address")]
        public void PostCartShouldBeRoutedCorrectly(
            string firstName,
            string lastName,
            string phoneNumber,
            string city,
            string address)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Cart")
                    .WithFormFields(new
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhoneNumber = phoneNumber,
                        City = city,
                        Address = address
                    }))
                .To<OrdersController>(c => c.Cart(new CartUserPersonalDataFormModel
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
        public void MadeSuccessfulOrderShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/MadeSuccessfulOrder")
                .To<OrdersController>(c => c.MadeSuccessfulOrder());

        [Fact]
        public void MineShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Orders")
                .To<OrdersController>(c => c.Mine());
    }
}
