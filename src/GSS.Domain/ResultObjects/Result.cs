namespace GSS.Domain.ResultObjects
{
    public record Result<TModel, TOperationResult> where TOperationResult : struct, Enum
    {
        public TModel? Model { get; init; }
        
        public TOperationResult OperationResult { get; init; }
    }
}
