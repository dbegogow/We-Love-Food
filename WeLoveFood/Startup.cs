using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Infrastructure;
using WeLoveFood.Services.Menus;
using WeLoveFood.Services.Users;
using WeLoveFood.Services.Cities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeLoveFood.Services.Restaurants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeLoveFood.Services.clients;

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

            services
                .AddControllersWithViews(options =>
                    {
                        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                    });

            services
                .AddTransient<IUsersService, UsersService>()
                .AddTransient<IMenusService, MenusService>()
                .AddTransient<ICitiesService, CitiesService>()
                .AddTransient<IClientsService, ClientsService>()
                .AddTransient<IRestaurantsService, RestaurantsService>();
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
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
