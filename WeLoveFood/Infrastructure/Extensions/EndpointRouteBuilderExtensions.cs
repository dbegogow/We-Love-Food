using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace WeLoveFood.Infrastructure.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapCustomControllerRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "All Cities",
                pattern: "/Cities",
                defaults: new { area = "", controller = "Cities", action = "All" });

            endpoints.MapControllerRoute(
                name: "All City Restaurants",
                pattern: "/Restaurants/{id}",
                defaults: new { area = "", controller = "Restaurants", action = "All" });

            endpoints.MapControllerRoute(
                name: "Restaurant Meals",
                pattern: "/Menu/{id}",
                defaults: new { area = "", controller = "Menus", action = "Meals" });

            endpoints.MapControllerRoute(
                name: "Cart",
                pattern: "/Cart",
                defaults: new { area = "", controller = "Orders", action = "Cart" });

            endpoints.MapControllerRoute(
                name: "Client Personal Data",
                pattern: "/PersonalData",
                defaults: new { area = "", controller = "Users", action = "PersonalData" });

            endpoints.MapControllerRoute(
                name: "Client Orders",
                pattern: "/Orders",
                defaults: new { area = "", controller = "Orders", action = "Mine" });

            endpoints.MapControllerRoute(
                name: "Client Favorite Restaurants",
                pattern: "/Favorite",
                defaults: new { area = "", controller = "Restaurants", action = "Favorite" });

            endpoints.MapControllerRoute(
                name: "Manager Orders",
                pattern: "/Manager/Orders/{id}",
                defaults: new { area = "Manager", controller = "Orders", action = "All" });

            endpoints.MapControllerRoute(
                name: "Manager Menu",
                pattern: "/Manager/Menu/{id}",
                defaults: new { area = "Manager", controller = "Menus", action = "Meals" });

            endpoints.MapControllerRoute(
                name: "Manager Add Meal",
                pattern: "/Manager/Meal/Add/{id}",
                defaults: new { area = "Manager", controller = "Menus", action = "AddMeal" });

            endpoints.MapControllerRoute(
                name: "Manager Add Meals Category",
                pattern: "/Manager/Category/Add/{id}",
                defaults: new { area = "Manager", controller = "Menus", action = "AddMealsCategory" });

            endpoints.MapControllerRoute(
                name: "Manager Edit Meals Category",
                pattern: "/Manager/Category/Edit/{id}",
                defaults: new { area = "Manager", controller = "Menus", action = "EditMealsCategory" });

            endpoints.MapControllerRoute(
                name: "Manager Restaurants",
                pattern: "/Manager/Restaurants",
                defaults: new { area = "Manager", controller = "Restaurants", action = "Mine" });

            endpoints.MapControllerRoute(
                name: "Manager Waiters",
                pattern: "/Manager/Waiters/{id}",
                defaults: new { area = "Manager", controller = "Waiters", action = "All" });

            endpoints.MapControllerRoute(
                name: "Waiter Orders",
                pattern: "/Waiter/Orders",
                defaults: new { area = "Waiter", controller = "Orders", action = "All" });
        }

        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        }
    }
}
