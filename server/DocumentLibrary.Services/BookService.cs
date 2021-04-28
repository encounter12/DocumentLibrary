using System;
using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Services.Contracts;

namespace DocumentLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        
        public BookService(IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
        }
        
        public async Task<BooksGridDto> GetBooksAsync(QueryFilterDto queryFilterDto) 
            => await _bookRepository.GetBooksAsync(queryFilterDto);
        
        public async Task<BookDetailsDto> GetBookDetailsAsync(long bookId)
            => await _bookRepository.GetBookDetailsAsync(bookId);

        public async Task<int> GetAllRecordsCountAsync()
            => await _bookRepository.GetAllRecordsCountAsync();

        public async Task<long> AddBookAsync(BookPostDto bookPostDto)
        {
            Genre genre = await _genreRepository.GetByIdAsync(bookPostDto.GenreId);

            if (genre == null)
            {
                throw new Exception($"Genre with Id: {bookPostDto.GenreId} could not be found");
            }
            
            long bookId = _bookRepository.AddBook(bookPostDto, genre);
            await _bookRepository.SaveChangesAsync();

            return bookId;
        }
        
        public async Task UpdateBookAsync(BookEditDto bookEditDto)
        {
            Genre genre = await _genreRepository.GetByIdAsync(bookEditDto.GenreId);

            if (genre == null)
            {
                throw new Exception($"Genre with Id: {bookEditDto.GenreId} could not be found");
            }
            
            //_bookRepository.UpdateBookByAttach(bookEditDto, genre);
            await _bookRepository.UpdateBookAsync(bookEditDto, genre);

            await _bookRepository.SaveChangesAsync();
        }

        public async Task DeleteBook(long bookId)
        {
            await _bookRepository.Delete(bookId);
            await _bookRepository.SaveChangesAsync();
        }
    }
}