using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Services.Managers;
using WeLoveFood.Web.Services.Orders;

namespace WeLoveFood.Web.Areas.Manager.Controllers
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
