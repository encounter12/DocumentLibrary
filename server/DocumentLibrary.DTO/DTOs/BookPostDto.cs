using System.Collections.Generic;

namespace DocumentLibrary.DTO.DTOs
{
    public class BookPostDto
    {
        public string Name { get; set; }
        
        public long GenreId { get; set; }
        
        public List<string> Keywords { get; set; }
        
        public string Description { get; set; }
    }
}