using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static WeLoveFood.WebConstants;

namespace WeLoveFood.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
