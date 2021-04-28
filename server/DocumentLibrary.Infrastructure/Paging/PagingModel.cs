namespace DocumentLibrary.Infrastructure.Paging
{
    public class PagingModel
    {
        public int PageNumber { get; set; }
        
        public int ItemsPerPage { get; set; }
        
        public int Skip { get; set; }
        
        public long PagesCount { get; set; }
    }
}