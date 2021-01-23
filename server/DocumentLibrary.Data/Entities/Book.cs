using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentLibrary.Data.Entities
{
    public class Book
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public Genre Genre { get; set; }
        
        public string Description { get; set; }
        
        public DateTime AvailabilityDate { get; set; }
        
        
        public string DownloadLink { get; set; }
        
        public ICollection<Keyword> Keywords { get; set; }
    }
}