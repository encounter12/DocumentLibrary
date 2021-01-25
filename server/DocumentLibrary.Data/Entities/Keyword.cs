namespace DocumentLibrary.Data.Entities
{
    public class Keyword
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual Book Book { get; set; }
    }
}