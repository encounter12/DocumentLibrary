using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Data.Repositories.Contracts
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<BookListDto>> GetBooksAsync();

        long AddBook(BookPostDto bookPostDto, Genre genre);

        Task UpdateBookAsync(BookEditDto bookEditDto, Genre genre);

        void UpdateBookByAttach(BookEditDto bookEditDto, Genre genre);

        Task<BooksGridDto> GetBooksAsync(QueryFilterDto queryFilterDto);
        
        Task<BookDetailsDto> GetBookDetailsAsync(long bookId);

        Task<int> GetAllRecordsCountAsync();
    }
}
