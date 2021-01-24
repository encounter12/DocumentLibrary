using System;
using System.Collections.Generic;
using System.Linq;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DocumentLibrary.Data.Seed
{
    public static class DbInitializer
    {
        public static DocumentLibraryContext BuildDbContext(
            IConfigurationRoot config, string connectionStringName, IUserService userService)
        {
            string documentLibraryConnectionString = config.GetConnectionString(connectionStringName);
            
            var optionsBuilder = new DbContextOptionsBuilder<DocumentLibraryContext>();

            optionsBuilder
                .UseSqlServer(documentLibraryConnectionString);
            
            var documentLibraryContext = 
                new DocumentLibraryContext(optionsBuilder.Options, userService);

            return documentLibraryContext;
        }
        
        public static void Seed(IConfigurationRoot config, string connectionStringName, IUserService userService)
        {
            
            DocumentLibraryContext dbContext = 
                BuildDbContext(config, connectionStringName, userService);
            
            dbContext.Database.EnsureCreated();

            if (dbContext.Books.Any())
            {
                return;
            };
            
            var sciFiGenre = new Genre
            {
                Name = "Sci-Fi"
            };
                    
            var fictionGenre = new Genre
            {
                Name = "Fiction"
            };
            
            var popularScience = new Genre
            {
                Name = "Popular science"
            };
                        
            var books = new List<Book>
            {
                new Book
                {
                    Name = "Space Odyssey",
                    Description = "One of the best sci-fi books in the world",
                    Genre = sciFiGenre,
                    Keywords = new List<Keyword>
                    {
                        new Keyword { Name = "adventure" },
                        new Keyword { Name = "space" },
                        new Keyword { Name = "artificial intelligence" },
                        new Keyword { Name = "astronaut" }
                    }
                },
                new Book
                {
                    Name = "Gone with the wind",
                    
                    Genre = fictionGenre,
                    Keywords = new List<Keyword>
                    {
                        new Keyword { Name = "crime" },
                        new Keyword { Name = "drama" }
                    }
                },
                new Book
                {
                    Name = "Brief history of time",
                    Genre = popularScience,
                    Keywords = new List<Keyword>
                    {
                        new Keyword { Name = "science" },
                        new Keyword { Name = "theoretical physics" },
                        new Keyword { Name = "quantum mechanics" }
                    },
                    AvailabilityDate = DateTime.Now.AddDays(15)
                }
            };
                    
            dbContext.Books.AddRange(books);
            dbContext.SaveChanges();
        }
    }
}