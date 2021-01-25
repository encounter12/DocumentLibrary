using System;
using System.Collections.Generic;
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
        
        public async Task<BooksGridDto> GetBooksAsync(int pageNumber, int itemsPerPage) 
            => await _bookRepository.GetBooksAsync(pageNumber, itemsPerPage);

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

        public async Task DeleteBook(long bookId)
        {
            await _bookRepository.Delete(bookId);
            await _bookRepository.SaveChangesAsync();
        }
    }
}