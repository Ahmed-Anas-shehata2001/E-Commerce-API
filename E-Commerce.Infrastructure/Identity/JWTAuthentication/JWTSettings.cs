namespace E_Commerce.Infrastructure.Identity.JWT
{

    public class JWTSettings
    {
        // it uses   HS 256   // make it 256 byte  32 chars at least
        public string Secret { get; set; } = default!;  // came from user secrets ( development )
        public string Issuer { get; set; } = default!; // came from appSettings.json
        public string Audience { get; set; } = default!;  

        public int AccessTokenMinutes { get; set; }

        public int RefreshTokenDays { get; set; }
    }
}
