using System;
using System.Collections.Generic;
using System.Linq;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Data.Identity;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext), 
                null, 
                null, 
                null, 
                null);

            bool adminRoleExists = roleManager.RoleExistsAsync(UserRoles.Admin.ToString()).Result;
            
            if (!adminRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString())).Wait();
            }

            var adminUser = new ApplicationUser
            {
                UserName = "john.doe",
                Email = "john.doe@gmail.com",
                EmailConfirmed = true
            };

            string password = "Qwerty123";

            var optionsInstance = new IdentityOptions
            {
                Password =
                {
                    RequireDigit = true
                }
            };
            
            IOptions<IdentityOptions> optionParameter = Options.Create(optionsInstance);

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(dbContext),
                optionParameter,
                new PasswordHasher<ApplicationUser>(), 
                null, 
                null, 
                null, 
                null, 
                null,
                null);
            
            var adminUserFromDb =  userManager.FindByNameAsync(adminUser.UserName).Result;

            if (adminUserFromDb == null)
            {
                IdentityResult result = userManager.CreateAsync(adminUser, password).Result;
            
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, UserRoles.Admin.ToString()).Wait();
                }
            }

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

            var standardUser = new User
            {
                Name = "Georgi"
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
                    },
                    BookCheckouts = new List<BookCheckout>
                    {
                        new BookCheckout
                        {
                            User = standardUser,
                            AvailabilityDate = DateTime.Now.AddDays(5)
                        }
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
                    }
                }
            };
                    
            dbContext.Books.AddRange(books);
            dbContext.SaveChanges();
        }
    }

    public enum UserRoles
    {
        Admin,
        User
    }
}