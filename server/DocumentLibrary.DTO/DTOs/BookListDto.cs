using System;

namespace DocumentLibrary.DTO.DTOs
{
    public class BookListDto
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Genre { get; set; }
        
        public DateTime PublicationDate { get; set; }
        
        public bool IsCheckedOut { get; set; }
    }
}