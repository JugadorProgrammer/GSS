namespace GSS.Domain.Entities
{
    public interface IDomainEntity<T>
    {
        T Id { get; }
    }
}
