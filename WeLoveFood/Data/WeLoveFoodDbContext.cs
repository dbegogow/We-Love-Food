using WeLoveFood.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WeLoveFood.Data
{
    public class WeLoveFoodDbContext : IdentityDbContext<User>
    {
        public WeLoveFoodDbContext(DbContextOptions<WeLoveFoodDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; init; }

        public DbSet<Restaurant> Restaurants { get; init; }

        public DbSet<Meal> Meals { get; init; }

        public DbSet<MealsCategory> MealsCategories { get; init; }

        public DbSet<Order> Orders { get; init; }

        public DbSet<Portion> Portions { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Restaurant>()
                .HasOne(r => r.City)
                .WithMany(c => c.Restaurants)
                .HasForeignKey(r => r.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MealsCategory>()
                .HasOne(mc => mc.Restaurant)
                .WithMany(r => r.MealsCategories)
                .HasForeignKey(mc => mc.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Meal>()
                .HasOne(m => m.MealsCategory)
                .WithMany(mc => mc.Meals)
                .HasForeignKey(m => m.MealsCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Portion>()
                .HasOne(p => p.Meal)
                .WithMany(m => m.Portions)
                .HasForeignKey(p => p.MealId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Portion>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Portions)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
