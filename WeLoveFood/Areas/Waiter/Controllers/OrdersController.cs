using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Orders;
using WeLoveFood.Web.Services.Waiters;
using static WeLoveFood.Web.TempDataConstants;

namespace WeLoveFood.Web.Areas.Waiter.Controllers
{
    public class OrdersController : WaiterController
    {
        private const int NoRestaurant = 0;

        private readonly IOrdersService _orders;
        private readonly IWaitersService _waiters;

        public OrdersController(
            IOrdersService orders,
            IWaitersService waiters)
        {
            this._orders = orders;
            this._waiters = waiters;
        }

        public IActionResult All()
        {
            var restaurantId = this._waiters
                .RestaurantId(User.Id());

            var orders = this._orders
                .RestaurantOrders(restaurantId);

            return View(orders);
        }

        public IActionResult Accept(int id)
        {
            var restaurantId = this._waiters
                .RestaurantId(User.Id());

            if (restaurantId == NoRestaurant)
            {
                return BadRequest();
            }

            this._orders
                .Accept(id);

            TempData[SuccessMessageKey] = SuccessfulAcceptedOrderMessage;

            return RedirectToAction("All", "Orders", new { area = "Waiter" });
        }
    }
}
