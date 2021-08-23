using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.Web.WebConstants;

namespace WeLoveFood.Web.Areas.Manager.Controllers
{
    [Area(ManagerAreaName)]
    [Authorize(Roles = ManagerRoleName)]
    public abstract class ManagerController : Controller
    {
    }
}