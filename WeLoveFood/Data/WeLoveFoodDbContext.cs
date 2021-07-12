using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeLoveFood.Data.Models;

namespace WeLoveFood.Data
{
    public class WeLoveFoodDbContext : IdentityDbContext
    {
        public WeLoveFoodDbContext(DbContextOptions<WeLoveFoodDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; init; }
    }
}
