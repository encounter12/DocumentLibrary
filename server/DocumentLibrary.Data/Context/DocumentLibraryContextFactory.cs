using System;
using Microsoft.EntityFrameworkCore;
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
                .AddJsonFile("Configuration/appdata.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();
            
            string documentLibraryConnectionString = config.GetConnectionString("DocumentLibraryConnection");
            
            var optionsBuilder = new DbContextOptionsBuilder<DocumentLibraryContext>();

            optionsBuilder
                .UseSqlServer(documentLibraryConnectionString);

            return new DocumentLibraryContext(optionsBuilder.Options);
        }
    }
}