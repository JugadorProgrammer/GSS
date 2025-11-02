using GSS.Domain.Auth;
using GSS.Domain.ConfigurationOptions;
using GSS.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GSS.Infrastructure.Auth
{
    public class JWTTokenAuthService : ITokenAuthService
    {
        private readonly JWTTokenOptions _jwtTokenOptions;

        public JWTTokenAuthService(IOptions<JWTTokenOptions> options)
        {
            _jwtTokenOptions = options.Value;
        }

        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtTokenOptions.Key);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Login),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = _jwtTokenOptions.Expires,
                Issuer = _jwtTokenOptions.Issuer,
                Audience = _jwtTokenOptions.Audience,
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
