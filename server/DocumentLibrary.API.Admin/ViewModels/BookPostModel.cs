using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DocumentLibrary.API.Admin.ViewModels
{
    public class BookPostModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        
        public long GenreId { get; set; }
        
        [MaxLength(4000)]
        public string Description { get; set; }
        
        public List<string> Keywords { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Keywords != null && Keywords.Any(k => k.Length > 40))
            {
                yield return new ValidationResult("Keyword length should not be greater than 40 characters");
            }
        }
    }
}