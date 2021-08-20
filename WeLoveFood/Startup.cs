using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Menus;
using WeLoveFood.Services.Carts;
using WeLoveFood.Services.Users;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Orders;
using WeLoveFood.Services.Images;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Waiters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using WeLoveFood.Services.Portions;
using WeLoveFood.Services.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeLoveFood.Services.Restaurants;
using Microsoft.Extensions.Configuration;
using WeLoveFood.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace WeLoveFood
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<WeLoveFoodDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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
                .AddTransient<IUsersService, UsersService>()
                .AddTransient<IMenusService, MenusService>()
                .AddTransient<ICartsService, CartsService>()
                .AddTransient<ICitiesService, CitiesService>()
                .AddTransient<IOrdersService, OrdersService>()
                .AddTransient<IImagesService, ImagesService>()
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