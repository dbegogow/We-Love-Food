using WeLoveFood.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WeLoveFood.Data
{
    public class WeLoveFoodDbContext : IdentityDbContext
    {
        public WeLoveFoodDbContext(DbContextOptions<WeLoveFoodDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; init; }

        public DbSet<Restaurant> Restaurants { get; init; }

        public DbSet<Meal> Meals { get; init; }

        public DbSet<Category> Categories { get; init; }
    }
}
