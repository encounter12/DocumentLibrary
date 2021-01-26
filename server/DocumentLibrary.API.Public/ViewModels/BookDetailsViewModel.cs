using System;
using System.Collections.Generic;

namespace DocumentLibrary.API.Public.ViewModels
{
    public class BookDetailsViewModel
    {
        public string Name { get; set; }
        
        public string Genre { get; set; }
        
        public List<string> Keywords { get; set; }
        
        public string Description { get; set; }
        
        public DateTime AvailabilityDate { get; set; }
        
        public string DownloadLink { get; set; }
    }
}