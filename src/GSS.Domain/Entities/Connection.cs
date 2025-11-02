namespace GSS.Domain.Entities
{
    public record Connection
    {
        public required string Id { get; init; }

        public required User User { get; init; }

        public required Group Group { get; init; }
    }
}
