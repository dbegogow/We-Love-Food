using System.Security.Claims;

using static WeLoveFood.WebConstants;

namespace WeLoveFood.Infrastructure.Extensions
{
    public static class ClaimsPrincipalsExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);

        public static bool IsManager(this ClaimsPrincipal user)
            => user.IsInRole(ManagerRoleName);

        public static bool IsWaiter(this ClaimsPrincipal user)
            => user.IsInRole(WaiterRoleName);

        public static bool IsClient(this ClaimsPrincipal user)
            => user.IsInRole(ClientRoleName);
    }
}