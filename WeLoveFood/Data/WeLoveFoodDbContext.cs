using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WeLoveFood.Data
{
    public class WeLoveFoodDbContext : IdentityDbContext
    {
        public WeLoveFoodDbContext(DbContextOptions<WeLoveFoodDbContext> options)
            : base(options)
        {
        }
    }
}
