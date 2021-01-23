using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentLibrary.Data.Entities
{
    public class Book
    {
        public Book()
        {
            this.Id = Guid.NewGuid();
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        public Guid GenreId { get; set; }
        
        public Genre Genre { get; set; }
        
        [MaxLength(5000)]
        public string Description { get; set; }
        
        public DateTime? AvailabilityDate { get; set; }
        
        public string DownloadLink { get; set; }
        
        public ICollection<Keyword> Keywords { get; set; }
    }
}