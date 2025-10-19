namespace GSS.Domain.Entities
{
    public record User
    {
        public required Guid Id { get; init; }

        public required string Login { get; init; }

        public required string Password { get; init; }
    }
}
