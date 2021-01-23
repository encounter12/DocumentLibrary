using System.Collections.Generic;
using System.Linq;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Entities;

namespace DocumentLibrary.Data.Seed
{
    public static class DbInitializer
    {
        public static void Seed(DocumentLibraryContext context)
        {
            context.Database.EnsureCreated();

            if (context.Books.Any())
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
                }
            };
                    
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}