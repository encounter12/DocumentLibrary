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
        
        public DbSet<Keyword> Keywords { get; set; }
    }
}