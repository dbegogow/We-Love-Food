using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Managers;
using WeLoveFood.Infrastructure.Extensions;

namespace WeLoveFood.Areas.Manager.Controllers
{
    public class WaitersController : ManagerController
    {
        private readonly IManagersService _managers;

        public WaitersController(IManagersService managers)
        {
            this._managers = managers;
        }

        public IActionResult All(int id)
        {
            var waiters = this._managers
                .Waiters(User.Id(), id)
                .ToList();

            return View(waiters);
        }
    }
}
