namespace DocumentLibrary.DTO.Config
{
    public class JwtConfig
    {
        public string ValidIssuer { get; set; }
        
        public string IssuerSigningKey { get; set; }
        
        public string ValidAudience { get; set; }
    }
}