namespace GSS.Domain.ResultObjects.OperationResults.User
{
    public enum UserCreateOperationResult : byte
    {
        Success,
        InvalidEmail,
        InvalidPassword,
        UserAlreadyExists
    }
}
