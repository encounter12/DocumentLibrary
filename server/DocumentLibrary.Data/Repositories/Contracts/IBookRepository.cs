using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Data.Repositories.Contracts
{
    public interface IBookRepository
    {
        Task<List<BookDto>> GetBooksAsync();
    }
}