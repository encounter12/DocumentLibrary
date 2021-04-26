using System.Collections.Generic;
using DocumentLibrary.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DocumentLibrary.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public string LastName { get; set; }
        
        public virtual ICollection<BookCheckout> BookCheckouts { get; set; }
    }
}