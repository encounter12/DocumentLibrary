namespace DocumentLibrary.DTO.Config
{
    public class AppData
    {
        public string DocumentLibraryConnectionString { get; set; }
        
        public JwtConfig JwtConfig { get; set; }
    }
}