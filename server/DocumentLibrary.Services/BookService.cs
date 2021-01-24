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
        
        public async Task<List<BookListDto>> GetBooksAsync() 
            => await _bookRepository.GetBooksAsync();

        public async Task AddBookAsync(BookPostDto bookPostDto)
        {
            Genre genre = await _genreRepository.GetByIdAsync(bookPostDto.GenreId);

            if (genre == null)
            {
                throw new Exception($"Genre with Id: {bookPostDto.GenreId} could not be found");
            }
            
            await _bookRepository.AddBookAsync(bookPostDto, genre);
            await _bookRepository.SaveChangesAsync();
        }
    }
}