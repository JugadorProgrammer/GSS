namespace GSS.Domain.ResultObjects
{
    public record Result<TEntity, TOperationResult> 
        where TEntity : class
        where TOperationResult : struct, Enum
    {
        public TEntity? Entity { get; init; }
        
        public TOperationResult OperationResult { get; init; }
    }
}
