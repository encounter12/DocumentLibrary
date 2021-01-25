using System;
using System.Collections.Generic;

namespace DocumentLibrary.Data.Entities
{
    public class Book
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual Genre Genre { get; set; }
        
        public string Description { get; set; }
        
        public DateTime AvailabilityDate { get; set; }
        
        public string DownloadLink { get; set; }
        
        public virtual ICollection<Keyword> Keywords { get; set; }
    }
}