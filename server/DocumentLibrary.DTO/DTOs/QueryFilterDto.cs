using System;

namespace DocumentLibrary.DTO.DTOs
{
    public class QueryFilterDto
    {
        public int? PageNumber { get; set; }
        
        public int? ItemsPerPage { get; set; }
        
        public string Search { get; set; }
        
        public DateTime? FromDate { get; set; }
        
        public DateTime? ToDate { get; set; }
        
        public string SortOrder { get; set; }
        
        public string SortBy { get; set; }
    }
}