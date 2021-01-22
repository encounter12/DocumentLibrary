using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;

namespace DocumentLibrary.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();

        IAsyncEnumerable<Pencil> GetPencilsAsAsyncEnumerable();
    }
}