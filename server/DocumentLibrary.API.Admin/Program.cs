using System;
using DocumentLibrary.Data.Seed;
using DocumentLibrary.Infrastructure.AspNetHelpers.UserService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DocumentLibrary.API.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("Configuration/appsettings-shared.json", optional: false, reloadOnChange: false)
                .Build();
            
            var host = CreateHostBuilder(args, configuration).Build();
            
            DbInitializer.Seed(
                configuration, "DocumentLibraryConnection", new UserServiceStartupTime());
            
            CreateHostBuilder(args, configuration).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration config) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(config);
                });
    }
}