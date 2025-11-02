namespace GSS.Domain.ConfigurationOptions
{
    public class JWTTokenOptions
    {
        public const string JWT = nameof(JWT);

        public required string Issuer { get; init; }

        public required string Audience { get; init; }

        public required string Key { get; init; }

        public required DateTime Expires { get; init; }
    }
}
