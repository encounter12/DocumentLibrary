namespace DocumentLibrary.API.Public.ViewModels
{
    public class BookListViewModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Genre { get; set; }
        
        public bool IsCheckedOut { get; set; }
    }
}