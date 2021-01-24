using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Infrastructure.DateTimeHelpers;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibrary.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly DocumentLibraryContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTimeHelper _dateTimeHelper;
        
        public BookRepository(DocumentLibraryContext context, IMapper mapper, IDateTimeHelper dateTimeHelper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
            _dateTimeHelper = dateTimeHelper;
        }

        public async Task<List<BookListDto>> GetBooksAsync()
        {
            DateTime dateTimeNow = _dateTimeHelper.GetDateTimeNow();
            
            var documents = await this
                .All()
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre.Name,
                    IsCheckedOut = b.AvailabilityDate > dateTimeNow
                })
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