using System.ComponentModel.DataAnnotations;

namespace GSS.Application.DTO
{
    public record AuthUserDTO
    {
        public required string Login { get; init; }

        public required string Password { get; init; }
    }
}
