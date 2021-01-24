using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Data.Repositories.Contracts
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<BookListDto>> GetBooksAsync();

        Task<Book> AddBookAsync(BookPostDto bookPostDto, Genre genre);
    }
}