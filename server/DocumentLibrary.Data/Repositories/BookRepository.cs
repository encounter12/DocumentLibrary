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
        
        public async Task<BooksGridDto> GetBooksAsync(int pageNumber, int itemsPerPage)
        {
            DateTime dateTimeNow = _dateTimeHelper.GetDateTimeNow();
            int skip = pageNumber * itemsPerPage - itemsPerPage;

            var documents = await this
                .All()
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre.Name,
                    IsCheckedOut = b.AvailabilityDate > dateTimeNow
                })
                .OrderByDescending(b => b.Id)
                .Skip(skip)
                .Take(itemsPerPage)
                .ToListAsync();

            int allRecordsCount = this.All().Count();
            var pagesCount = allRecordsCount % itemsPerPage > 0 ? 
                (allRecordsCount / itemsPerPage) + 1 : (allRecordsCount / itemsPerPage);

            var responseModel = new BooksGridDto
            {
                BooksList = documents,
                PagesCount = pagesCount
            };

            return responseModel;
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

        public long AddBook(BookPostDto bookPostDto, Genre genre)
        {
            var book = _mapper.Map<Book>(bookPostDto);
            book.Genre = genre;
            
            Add(book);
            return book.Id;
        }
    }
}