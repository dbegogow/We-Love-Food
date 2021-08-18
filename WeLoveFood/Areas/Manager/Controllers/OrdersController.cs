using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Managers;
using WeLoveFood.Infrastructure.Extensions;
using WeLoveFood.Services.Orders;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class OrdersController : ManagerController
    {
        private readonly IOrdersService _orders;
        private readonly IManagersService _managers;

        public OrdersController(
            IOrdersService orders,
            IManagersService managers)
        {
            this._orders = orders;
            this._managers = managers;
        }

        public IActionResult All(int id)
        {
            var hasRestaurant = this._managers
                .HasRestaurant(User.Id(), id);

            if (!hasRestaurant)
            {
                return BadRequest();
            }

            var orders = this._orders
                .RestaurantOrders(id);

            ViewBag.RestaurantId = id;

            return View(orders);
        }
    }
}
