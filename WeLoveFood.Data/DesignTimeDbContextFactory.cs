using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using static WeLoveFood.Web.Data.DbConfiguration;

namespace WeLoveFood.Web.Data
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