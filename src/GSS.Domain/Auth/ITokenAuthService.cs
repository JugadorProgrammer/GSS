using GSS.Domain.Entities;

namespace GSS.Domain.Auth
{
    public interface ITokenAuthService
    {
        string GetToken(User user);
    }
}
