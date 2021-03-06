using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using static WeLoveFood.Data.DbConfiguration;

namespace WeLoveFood.Data
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeLoveFoodDbContext>
    {
        public WeLoveFoodDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WeLoveFoodDbContext>();

            var connectionString = ConnectionString;

            builder.UseSqlServer(connectionString);

            return new WeLoveFoodDbContext(builder.Options);
        }
    }
}