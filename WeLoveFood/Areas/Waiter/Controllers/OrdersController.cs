using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Orders;
using WeLoveFood.Services.Waiters;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Areas.Waiter.Controllers
{
    public class OrdersController : WaiterController
    {
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
    }
}
