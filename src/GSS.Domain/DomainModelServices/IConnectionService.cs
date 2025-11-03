using GSS.Domain.Entities;
using GSS.Domain.ResultObjects;
using GSS.Domain.ResultObjects.OperationResults.Connection;

namespace GSS.Domain.DomainModelServices
{
    public interface IConnectionService
    {
        Task<ConnetionOperationResult> CreateConnection(string connectionId, Guid userId, Guid groupId);
        
        Task<Result<Connection, ConnetionOperationResult>> GetConnection(string connectionId);

        Task<ConnetionOperationResult> RemoveConnection(string connectionId);
    }
}
