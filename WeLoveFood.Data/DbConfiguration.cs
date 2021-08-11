using System.IO;
using Microsoft.Extensions.Configuration;

namespace WeLoveFood.Data
{
    public static class DbConfiguration
    {
        public static string GetConfigurationString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
