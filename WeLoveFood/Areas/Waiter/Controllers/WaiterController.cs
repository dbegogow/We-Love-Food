using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.WebConstants;

namespace WeLoveFood.Areas.Waiter.Controllers
{
    [Area(WaiterAreaName)]
    [Authorize(Roles = WaiterRoleName)]
    public abstract class WaiterController : Controller
    {
    }
}