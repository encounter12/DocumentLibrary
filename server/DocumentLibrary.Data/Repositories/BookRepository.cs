using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Data.Repositories.Constants;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Infrastructure.DateTimeHelpers;
using DocumentLibrary.Infrastructure.Paging;
using DocumentLibrary.Infrastructure.Sorting;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibrary.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly DocumentLibraryContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPagingService _pagingService;
        private readonly ISortingService _sortingService;

        private static List<string> SortColumns => new()
        {
            SortColumnConstants.PublicationDate,
            SortColumnConstants.BookName,
            SortColumnConstants.BookGenre,
        };
        
        public BookRepository(
            DocumentLibraryContext context,
            IMapper mapper,
            IDateTimeHelper dateTimeHelper,
            IPagingService pagingService,
            ISortingService sortingService)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
            _dateTimeHelper = dateTimeHelper;
            _pagingService = pagingService;
            _sortingService = sortingService;
        }
        
        public async Task<BooksGridDto> GetBooksAsync(QueryFilterDto queryFilter)
        {
            DateTime dateTimeNow = _dateTimeHelper.GetDateTimeNow();
            
            IQueryable<BookListDto> booksQueryable = this
                .All()
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre.Name,
                    IsCheckedOut = b.BookCheckouts
                        .Select(bc => bc.AvailabilityDate)
                        .OrderByDescending(bcd => bcd)
                        .FirstOrDefault() > dateTimeNow
                });
            
            booksQueryable = FilterBooks(
                booksQueryable,
                queryFilter.Search,
                queryFilter.FromDate,
                queryFilter.ToDate);
            
            string defaultSortColumn = SortColumnConstants.PublicationDate;
            
            string dynamicSortString = _sortingService.BuildSortString(
                queryFilter.SortBy,
                queryFilter.SortOrder,
                SortColumns,
                defaultSortColumn);
            
            // OrderBy - By System.Linq.Dynamic.Core
            booksQueryable = booksQueryable.OrderBy(dynamicSortString);
            
            long totalItems = await booksQueryable.LongCountAsync();
            
            PagingModel pagingModel = _pagingService.GetPagingModel(
                queryFilter.PageNumber,
                queryFilter.ItemsPerPage,
                totalItems);

            var books = await booksQueryable
                .Skip(pagingModel.Skip)
                .Take(pagingModel.ItemsPerPage)
                .ToListAsync();

            var booksGridDto = new BooksGridDto
            {
                BooksList = books,
                Pagination = new Pagination
                {
                    PageNumber = pagingModel.PageNumber,
                    ItemsPerPage = pagingModel.ItemsPerPage,
                    PagesCount = pagingModel.PagesCount,
                    TotalItems = totalItems
                }
            };
            
            return booksGridDto;
        }

        public async Task<BookDetailsDto> GetBookDetailsAsync(long bookId)
        {
            Book book = await this.GetByIdAsync(bookId);

            BookDetailsDto bookDetailsDto = _mapper.Map<BookDetailsDto>(book);

            return bookDetailsDto;
        }

        public async Task<int> GetAllRecordsCountAsync()
            => await All().CountAsync();
        
        public async Task<List<BookListDto>> GetBooksAsync()
        {
            DateTime dateTimeNow = _dateTimeHelper.GetDateTimeNow();
            
            var books = await All()
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre.Name,
                    IsCheckedOut = b.BookCheckouts
                        .Select(bc => bc.AvailabilityDate)
                        .OrderByDescending(bcd => bcd)
                        .FirstOrDefault() > dateTimeNow
                })
                .ToListAsync();

            return books;
        }

        public long AddBook(BookPostDto bookPostDto, Genre genre)
        {
            var book = _mapper.Map<Book>(bookPostDto);
            book.Genre = genre;
            
            Add(book);
            return book.Id;
        }
        
        private IQueryable<BookListDto> FilterBooks(
            IQueryable<BookListDto> books, string search, DateTime? from, DateTime? to)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                books = books
                    .Where(a =>
                        a.Name.Contains(search) ||
                        a.Genre.Contains(search));
            }
            
            if (from.HasValue)
            {
                books = books.Where(a => a.PublicationDate >= from);
            }
            
            if (to.HasValue)
            {
                books = books.Where(a => a.PublicationDate <= to);
            }

            return books;
        }
    }
}