using GSS.Domain.Entities;

namespace GSS.Domain.Repositories
{
    public interface IDataBaseRepository
    {
        Task<User?> GetUser(string name, string paswword);

        Task<User> CreateUser(string name, string paswword);

        Task CreateConnection(Connection connection);

        Task<Group> CreateGroup(string name);
    }
}
