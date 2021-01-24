using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public BookRepository(DocumentLibraryContext context, IMapper mapper): base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookListDto>> GetBooksAsync()
        {
            var documents = await this
                .All()
                .ProjectTo<BookListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return documents;
        }

        public async Task<Book> AddBookAsync(BookPostDto bookPostDto, Genre genre)
        {
            var book = _mapper.Map<Book>(bookPostDto);
            book.Genre = genre;
            
            await AddAsync(book);
            return book;
        }
    }
}