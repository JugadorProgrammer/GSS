using GSS.Domain.Entities;
using GSS.Domain.ResultObjects;
using GSS.Domain.ResultObjects.OperationResults.Group;

namespace GSS.Domain.DomainModelServices
{
    public interface IGroupService
    {
        Task<Result<Group, GroupCreateOperationResult>> CreateGroup(string name);

        Task<Result<Group, GroupGetOperationResult>> GetGroup(Guid id);
    }
}
