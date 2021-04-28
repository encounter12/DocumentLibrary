using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Services.Contracts
{
    public interface IBookService
    {
        Task<BooksGridDto> GetBooksAsync(QueryFilterDto queryFilterDto);

        Task<BookDetailsDto> GetBookDetailsAsync(long bookId);

        Task<int> GetAllRecordsCountAsync();

        Task<long> AddBookAsync(BookPostDto bookPostDto);

        Task DeleteBook(long bookId);
    }
}