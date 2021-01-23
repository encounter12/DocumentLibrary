using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentLibrary.Data.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        
        public ICollection<Book> Books { get; set; }
    }
}