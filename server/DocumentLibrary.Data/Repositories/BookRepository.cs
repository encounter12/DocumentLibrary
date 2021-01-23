using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibrary.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly DocumentLibraryContext _context;

        public BookRepository(DocumentLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookDto>> GetBooksAsync()
        {
            var documents = await this
                .All()
                .Select(b => new BookDto
                {
                    Name = b.Name,
                    Genre = b.Genre.Name
                })
                .ToListAsync();

            return documents;
        }
    }
}