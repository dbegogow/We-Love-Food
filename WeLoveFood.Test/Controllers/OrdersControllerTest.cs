using System.Collections.Generic;
using Xunit;
using System.Linq;
using WeLoveFood.Data.Models;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Services.Models.Orders;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Carts;

using HttpMethod = System.Net.Http.HttpMethod;

using static WeLoveFood.Web.WebConstants;
using static WeLoveFood.Test.Data.CartTestData;
using static WeLoveFood.Test.Data.OrdersTestData;

namespace WeLoveFood.Test.Controllers
{
    public class OrdersControllerTest
    {
        [Fact]
        public void GetCartShouldBeForAuthorizedClientsUsers()
            => MyController<OrdersController>
                .Instance()
                .Calling(c => c.Cart())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(ClientRoleName));

        [Theory]
        [InlineData(3, 32)]
        [InlineData(4, 52)]
        [InlineData(6, 107)]

        public void GetCartShouldReturnCorrectViewWithValidModelWithPortionsAndUserData(
            int portionsCount,
            int expectedTotalPrice)
            => MyController<OrdersController>
                .Instance(controller => controller
                    .WithUser("ClientId", "Client")
                    .WithData(GetCart(portionsCount)))
                .Calling(c => c.Cart())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CartViewModel>()
                    .Passing(model => model.CartAllPortions.Portions.Count() == portionsCount &&
                                      model.CartAllPortions.DeliveryFee == 2 &&
                                      model.CartAllPortions.TotalPrice == expectedTotalPrice));

        [Fact]
        public void PostCartShouldBeForAuthorizedClientsUsersAndShouldHaveRestrictionsForHttpGetOnly()
            => MyController<OrdersController>
                .Instance()
                .Calling(c => c.Cart(new CartUserPersonalDataFormModel()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(ClientRoleName)
                    .RestrictingForHttpMethod(HttpMethod.Post));


        [Fact]
        public void PostCartShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<OrdersController>
                .Calling(c => c.Cart(new CartUserPersonalDataFormModel()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CartViewModel>());


        [Theory]
        [InlineData("Test first name", "Test last name", "0884184241", "Test City", "Test address")]
        public void PostCartShouldSaveOrderSetTempDataMessageAndRedirectWhenValidModel(
            string firstName,
            string lastName,
            string phoneNumber,
            string city,
            string address)
            => MyController<OrdersController>
                .Instance(controller => controller
                    .WithUser("ClientId", "Client")
                    .WithData(GetCart(3)))
                .Calling(c => c.Cart(new CartUserPersonalDataFormModel
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
                    .WithSet<Order>(set => set != null &&
                                           set.FirstOrDefault(o => o.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<OrdersController>(c => c.MadeSuccessfulOrder()));

        [Fact]
        public void MadeSuccessfulOrderShouldBeForAuthorizedClientsUsers()
            => MyController<OrdersController>
                .Instance()
                .Calling(c => c.MadeSuccessfulOrder())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(ClientRoleName));

        [Fact]
        public void MineShouldBeForAuthorizedClientsUsers()
            => MyController<OrdersController>
                .Instance(controller => controller
                    .WithUser("ClientId", "Client")
                    .WithData(GetOrders()))
                .Calling(c => c.Mine())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<ClientOrderServiceModel>>()
                    .Passing(model => model.Count() == 3));
    }
}