namespace DocumentLibrary.DTO.DTOs
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        
        public int ItemsPerPage { get; set; }
        
        public long PagesCount { get; set; }
        
        public long TotalItems { get; set; }
    }
}