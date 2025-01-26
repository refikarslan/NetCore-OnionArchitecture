using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore_OnionArchitecture.Application.DTOs;
using NetCore_OnionArchitecture.Domain.Common.Repositories;
using NetCore_OnionArchitecture.Domain.Entities;

namespace NetCore_OnionArchitecture.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        { 
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _authRepository.Login(loginUserDto.UserName, loginUserDto.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid UserName or password" });
            }

            string token = _authRepository.CreateToken(user);

            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {

            if (String.IsNullOrEmpty(registerUserDto.Name) || String.IsNullOrEmpty(registerUserDto.UserName) || String.IsNullOrEmpty(registerUserDto.Password))
            {
                return BadRequest(new { message = "Name, UserName, and Password are required" });
            }

            User user = new User
            {
                UserName = registerUserDto.UserName,
                Name = registerUserDto.Name,
                Password = registerUserDto.Password,
                Role = registerUserDto.Role,
                EMail = registerUserDto.EMail
            };
                                                      
            User registeredUser = await _authRepository.Register(user);

            if (registeredUser == null)
            {
                return BadRequest(new { message = "User registration unsuccessful" });
            }

            string token = _authRepository.CreateToken(registeredUser);

            return Ok(new { Token = token });
            
        }

    }
}
