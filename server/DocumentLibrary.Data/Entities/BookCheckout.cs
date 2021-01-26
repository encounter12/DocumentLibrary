using System;

namespace DocumentLibrary.Data.Entities
{
    public class BookCheckout
    {
        public long Id { get; set; }
        
        public long BookId { get; set; }
        public virtual Book Book { get; set; }
        
        public long UserId { get; set; }
        public virtual User User { get; set; }
        
        public DateTime AvailabilityDate { get; set; }
    }
}