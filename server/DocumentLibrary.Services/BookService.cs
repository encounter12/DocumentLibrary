using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Services.Contracts;

namespace DocumentLibrary.Services
{
    public class BookService : IBookService
    {
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            // in repository method you should use: .ToListAsync()
            await Task.Delay(1000);
            IEnumerable<Book> books = new List<Book>
            {
                new Book
                {
                    Name = "John Doe"
                }
            };

            return books;
        }
        
        public async IAsyncEnumerable<Pencil> GetPencilsAsAsyncEnumerable()
        {
            //in repository method you should use: .AsAsyncEnumerable()
            yield return new Pencil() { Brand = "American Best Pencil"};
            await Task.Delay(1000);
            yield return new Pencil() { Brand = "Bulgarian Best Pencil"};
        }
    }
}