using System;
using DocumentLibrary.Data.Identity;

namespace DocumentLibrary.Data.Entities
{
    public class BookCheckout
    {
        public long Id { get; set; }
        
        public long BookId { get; set; }
        
        public virtual Book Book { get; set; }
        
        public long ApplicationUserId { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public DateTime AvailabilityDate { get; set; }
    }
}