using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Services.Contracts;

namespace DocumentLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public async Task<List<BookDto>> GetBooksAsync() 
            => await _bookRepository.GetBooksAsync();
    }
}