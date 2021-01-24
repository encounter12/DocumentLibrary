using System;
using DocumentLibrary.Data.Seed;
using DocumentLibrary.Infrastructure.AspNetHelpers.UserService;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DocumentLibrary.Data.Context
{
    public class DocumentLibraryContextFactory: IDesignTimeDbContextFactory<DocumentLibraryContext>
    {
        public DocumentLibraryContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("Configuration/appsettings-shared.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();

            DocumentLibraryContext dbContext = DbInitializer.BuildDbContext(
                config, "DocumentLibraryConnection", new UserServiceDesignTime());

            return dbContext;
        }
    }
}