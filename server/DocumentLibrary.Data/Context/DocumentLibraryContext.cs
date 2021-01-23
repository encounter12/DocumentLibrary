using DocumentLibrary.Data.Context.EntityTypeConfiguration;
using DocumentLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibrary.Data.Context
{
    public class DocumentLibraryContext : DbContext
    {
        public DocumentLibraryContext(DbContextOptions<DocumentLibraryContext> options) : 
            base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        
        public DbSet<Genre> Genres { get; set; }
        
        public DbSet<Keyword> Keywords { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
            new GenreEntityTypeConfiguration().Configure(modelBuilder.Entity<Genre>());
            new KeywordEntityTypeConfiguration().Configure(modelBuilder.Entity<Keyword>());
        }
    }
}