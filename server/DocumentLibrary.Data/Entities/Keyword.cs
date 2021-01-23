namespace DocumentLibrary.Data.Entities
{
    public class Keyword
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public Book Book { get; set; }
    }
}