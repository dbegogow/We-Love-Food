using WeLoveFood.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using static WeLoveFood.Data.DbConfiguration;

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

        public DbSet<Client> Clients { get; init; }

        public DbSet<Cart> Carts { get; init; }

        public DbSet<Manager> Managers { get; init; }

        public DbSet<Waiter> Waiters { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Restaurant>()
                .HasOne(r => r.City)
                .WithMany(c => c.Restaurants)
                .HasForeignKey(r => r.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Restaurant>()
                .HasOne(r => r.Manager)
                .WithMany(m => m.Restaurants)
                .HasForeignKey(r => r.ManagerId)
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
                .Entity<Portion>()
                .HasOne(p => p.Cart)
                .WithMany(c => c.Portions)
                .HasForeignKey(p => p.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Client>()
                .HasMany(c => c.Restaurants)
                .WithMany(r => r.Clients);

            builder
                .Entity<Client>()
                .HasOne(c => c.Cart)
                .WithOne(c => c.Client)
                .HasForeignKey<Cart>(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Waiter>()
                .HasOne(w => w.Manager)
                .WithMany(m => m.Waiters)
                .HasForeignKey(w => w.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Waiter>()
                .HasOne(w => w.Restaurant)
                .WithMany(r => r.Waiters)
                .HasForeignKey(w => w.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
