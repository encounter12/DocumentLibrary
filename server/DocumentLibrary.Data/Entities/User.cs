using System.Collections.Generic;

namespace DocumentLibrary.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual ICollection<BookCheckout> BookCheckouts { get; set; }
    }
}