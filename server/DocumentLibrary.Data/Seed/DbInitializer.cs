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

            SeedRole(roleManager, UserRoles.Admin);
            SeedRole(roleManager, UserRoles.StandardUser);

            var adminUser = new ApplicationUser
            {
                UserName = "john.doe",
                Email = "john.doe@gmail.com",
                EmailConfirmed = true,
                FirstName = "John",
                LastName = "Doe"
            };

            string adminUserPassword = "Qwerty123";
            
            var standardUser = new ApplicationUser
            {
                UserName = "michael.jordan",
                Email = "michael.jordan@gmail.com",
                EmailConfirmed = true,
                FirstName = "Michael",
                LastName = "Jordan"
            };

            string standardUserPassword = "Qwerty456";

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

            SeedUser(userManager, adminUser, adminUserPassword, UserRoles.Admin);
            SeedUser(userManager, standardUser, standardUserPassword, UserRoles.StandardUser);

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
            
            var tragedy = new Genre
            {
                Name = "Tragedy"
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
                    PublicationDate = new DateTime(1968, 8, 23),
                    BookCheckouts = new List<BookCheckout>
                    {
                        new BookCheckout
                        {
                            ApplicationUser = standardUser,
                            AvailabilityDate = DateTime.Now.AddDays(5)
                        }
                    }
                },
                new Book
                {
                    Name = "Gone with the wind",
                    Description = "One of the top fiction books of the 20th century",
                    Genre = fictionGenre,
                    Keywords = new List<Keyword>
                    {
                        new Keyword { Name = "crime" },
                        new Keyword { Name = "drama" }
                    },
                    PublicationDate = new DateTime(1936, 6, 30)
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
                    PublicationDate = new DateTime(1988, 7, 29)
                },
                new Book
                {
                    Name = "The Great Gatsby",
                    Description = "The Great Gatsby provides a critical social history of Prohibition-era America during the Jazz Age",
                    Genre = tragedy,
                    Keywords = new List<Keyword>
                    {
                        new Keyword { Name = "tragedy" },
                        new Keyword { Name = "jazz era" }
                    },
                    PublicationDate = new DateTime(1925, 11, 16)
                }
            };
                    
            dbContext.Books.AddRange(books);
            dbContext.SaveChanges();
        }
        
        private static void SeedUser(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string password,
            UserRoles roleName)
        {
            var adminUserFromDb =  userManager.FindByNameAsync(user.UserName).Result;

            if (adminUserFromDb == null)
            {
                IdentityResult result = userManager.CreateAsync(user, password).Result;
            
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, roleName.ToString()).Wait();
                }
            }
        }

        private static void SeedRole(RoleManager<IdentityRole> roleManager, UserRoles roleName)
        {
            bool roleExists = roleManager.RoleExistsAsync(roleName.ToString()).Result;
            
            if (!roleExists)
            {
                roleManager.CreateAsync(new IdentityRole(roleName.ToString())).Wait();
            }
        }
    }
    
    public enum UserRoles
    {
        Admin,
        StandardUser
    }
}