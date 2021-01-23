using System;

namespace DocumentLibrary.Data.Entities
{
    public class Keyword
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid BookId { get; set; }
        
        public Book Book { get; set; }
    }
}