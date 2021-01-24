using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Services.Contracts
{
    public interface IBookService
    {
        Task<List<BookListDto>> GetBooksAsync();

        Task<long> AddBookAsync(BookPostDto bookPostDto);
    }
}