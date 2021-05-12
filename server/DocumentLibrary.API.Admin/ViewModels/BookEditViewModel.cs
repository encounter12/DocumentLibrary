namespace DocumentLibrary.API.Admin.ViewModels
{
    public class BookEditViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public long GenreId { get; set; }
        
        public string Description { get; set; }
    }
}