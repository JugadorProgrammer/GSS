namespace GSS.Domain.Entities
{
    public record Group : IDomainEntity<Guid>
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }
    }
}
