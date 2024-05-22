namespace TruckMove.API.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
    }
}
