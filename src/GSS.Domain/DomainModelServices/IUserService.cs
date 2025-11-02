using GSS.Domain.Entities;
using GSS.Domain.ResultObjects;
using GSS.Domain.ResultObjects.OperationResults.User;

namespace GSS.Domain.DomainModelServices
{
    public interface IUserService
    {
        Task<Result<User, UserGetOperationResult>> GetUser(string login, string password);

        Task<Result<User, UserCreateOperationResult>> CreateUser(string login, string password);
    }
}
