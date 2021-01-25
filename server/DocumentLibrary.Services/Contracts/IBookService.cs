using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Services.Contracts
{
    public interface IBookService
    {
        Task<BooksGridDto> GetBooksAsync(int pageNumber, int itemsPerPage);

        Task<int> GetAllRecordsCountAsync();

        Task<long> AddBookAsync(BookPostDto bookPostDto);

        Task DeleteBook(long bookId);
    }
}