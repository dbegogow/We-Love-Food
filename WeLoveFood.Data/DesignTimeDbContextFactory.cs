using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WeLoveFood.Data
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeLoveFoodDbContext>
    {
        public WeLoveFoodDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WeLoveFoodDbContext>();

            var connectionString = DbConfiguration.GetConfigurationString();

            builder.UseSqlServer(connectionString);

            return new WeLoveFoodDbContext(builder.Options);
        }
    }
}
