namespace Backend_developer_Test.Models.Configuration
{
    public class Configurations
    {
    }
    public class JWTConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string Subject { get; set; }
        public int access_token_expiration { get; set; }
        public int refresh_token_expiration { get; set; }
    }
}
