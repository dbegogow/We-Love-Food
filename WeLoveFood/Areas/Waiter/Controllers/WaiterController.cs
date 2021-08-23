using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.Web.WebConstants;

namespace WeLoveFood.Web.Areas.Waiter.Controllers
{
    [Area(WaiterAreaName)]
    [Authorize(Roles = WaiterRoleName)]
    public abstract class WaiterController : Controller
    {
    }
}