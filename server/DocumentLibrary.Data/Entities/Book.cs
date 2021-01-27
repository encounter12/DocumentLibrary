using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentLibrary.Data.Entities
{
    public class Book
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual Genre Genre { get; set; }
        
        public string Description { get; set; }
        
        public string DownloadLink { get; set; }
        
        public virtual ICollection<Keyword> Keywords { get; set; }
        
        public virtual ICollection<BookCheckout> BookCheckouts { get; set; }
    }
}