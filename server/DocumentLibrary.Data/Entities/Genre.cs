using System.Collections.Generic;

namespace DocumentLibrary.Data.Entities
{
    public class Genre
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<Book> Books { get; set; }
    }
}