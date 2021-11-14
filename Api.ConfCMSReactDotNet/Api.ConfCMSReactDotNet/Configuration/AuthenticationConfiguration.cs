namespace Api.ConfCMSReactDotNet.Configuration
{
    public class AuthenticationConfiguration
    {
        public JwtBearerConfiguration JwtBearerConfiguration { get; set; } = new JwtBearerConfiguration();
    }

    public class JwtBearerConfiguration
    {
        public string Authority { get; set; } = string.Empty;

        public TokenValidationConfiguration TokenValidationConfiguration { get; set; } =
            new TokenValidationConfiguration();
    }

    public class TokenValidationConfiguration
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
