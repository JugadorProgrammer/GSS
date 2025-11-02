using GSS.Application.DTO;
using GSS.Domain.Auth;
using GSS.Domain.DomainModelServices;
using GSS.Domain.ResultObjects.OperationResults.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSS.Application.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenAuthService _tokenAuthService;

        public AuthController(IUserService userService, ITokenAuthService tokenAuthService)
        {
            _userService = userService;
            _tokenAuthService = tokenAuthService;
        }

        [Authorize]
        [Route("check")]
        public IActionResult AuthCheck()
        {
            return Ok();
        }

        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthUserDTO userDTO)
        {
            var result = await _userService.GetUser(userDTO.Login, userDTO.Password);
            if (result.OperationResult == UserGetOperationResult.UserNotFound)
            {
                return BadRequest("User not found");
            }

            var user = result.Entity;
            if (user is null)
            {
                return StatusCode(500, "Internal Server Error");
            }

            var token = _tokenAuthService.GetToken(user);
            return Ok(token);
        }

        [Route("singup")]
        public async Task<IActionResult> SingUp([FromBody] AuthUserDTO userDTO)
        {
            var result = await _userService.CreateUser(userDTO.Login, userDTO.Password);
            switch (result.OperationResult)
            {
                case UserCreateOperationResult.InvalidEmail:
                    return BadRequest("Invalid emal format");
                case UserCreateOperationResult.InvalidPassword:
                    return BadRequest("Invalid password format");
                case UserCreateOperationResult.UserAlreadyExists:
                    return BadRequest("User already exists");
            }

            var user = result.Entity;
            if (user is null)
            {
                return StatusCode(500, "Internal Server Error");
            }

            var token = _tokenAuthService.GetToken(user);
            return Ok(token);
        }
    }
}
