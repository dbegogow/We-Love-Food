using WeLoveFood.Web.Data;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Web.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using WeLoveFood.Web.Services.Carts;
using WeLoveFood.Web.Services.Menus;
using WeLoveFood.Web.Services.Users;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Services.Orders;
using WeLoveFood.Web.Services.Waiters;
using WeLoveFood.Web.Services.clients;
using WeLoveFood.Web.Services.Managers;
using WeLoveFood.Web.Services.Portions;
using Microsoft.Extensions.Configuration;
using WeLoveFood.Web.Services.Restaurants;
using Microsoft.Extensions.DependencyInjection;
using WeLoveFood.Web.Web.Infrastructure.Extensions;
using WeLoveFood.Web.Web.Infrastructure.UploadFiles;

namespace WeLoveFood.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<WeLoveFoodDbContext>();

            services.
                AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WeLoveFoodDbContext>();
            
            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            services
                .AddTransient<IImages, Images>()
                .AddTransient<IUsersService, UsersService>()
                .AddTransient<IMenusService, MenusService>()
                .AddTransient<ICartsService, CartsService>()
                .AddTransient<ICitiesService, CitiesService>()
                .AddTransient<IOrdersService, OrdersService>()
                .AddTransient<IClientsService, ClientsService>()
                .AddTransient<IWaitersService, WaitersService>()
                .AddTransient<IPortionsService, PortionsService>()
                .AddTransient<IManagersService, ManagersService>()
                .AddTransient<IRestaurantsService, RestaurantsService>();

            services.AddRazorPages(options =>
            {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/Register");
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Login");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapCustomControllerRoutes();
                    endpoints.MapDefaultAreaRoute();

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}