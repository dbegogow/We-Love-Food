using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.WebConstants;

namespace WeLoveFood.Areas.Manager.Controllers
{
    [Area(ManagerAreaName)]
    [Authorize(Roles = ManagerRoleName)]
    public abstract class ManagerController : Controller
    {
    }
}
