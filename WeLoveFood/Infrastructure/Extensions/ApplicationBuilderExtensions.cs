using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WeLoveFood.Web.Data;
using WeLoveFood.Web.Data.Models;
using static WeLoveFood.Web.WebConstants;

namespace WeLoveFood.Web.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            CreateRole(services, AdministratorRoleName);
            SeedAdministrator(services);
            CreateRole(services, ManagerRoleName);
            CreateRole(services, WaiterRoleName);
            CreateRole(services, ClientRoleName);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services
                .GetRequiredService<WeLoveFoodDbContext>();

            data?.Database.Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();

            Task
                .Run(async () =>
                {
                    const string adminEmail = "admin_wlf@gmail.com";
                    const string adminPassword = "Adminwlf1";

                    var admin = await userManager.FindByEmailAsync(adminEmail);

                    if (admin != null)
                    {
                        return;
                    }

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, AdministratorRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void CreateRole(IServiceProvider services, string roleName)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(roleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = roleName };

                    await roleManager.CreateAsync(role);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}