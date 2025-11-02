namespace GSS.Domain.Entities
{
    public record User : IDomainEntity<Guid>
    {
        public required Guid Id { get; init; }

        public required string Login { get; init; }

        public required string Password { get; init; }
    }
}
