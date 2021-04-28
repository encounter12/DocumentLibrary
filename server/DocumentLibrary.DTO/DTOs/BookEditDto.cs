namespace DocumentLibrary.DTO.DTOs
{
    public class BookEditDto
    {
        public long Id { get; }
        
        public string Name { get; set; }
        
        public long GenreId { get; set; }
        
        public string Description { get; set; }
    }
}