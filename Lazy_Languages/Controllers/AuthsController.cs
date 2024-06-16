using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using NTTRUNG_Lazy_Languages_Application.Service;
using NTTRUNG_Lazy_Languages_Application.Interface.Service;
using NTTRUNG_Lazy_Languages_Application.Dtos.Entity.Account;
namespace NTTRUNG_Lazy_Languages_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginModel)
        {
            var token = await _authService.AuthenticateUser(loginModel);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerModel)
        {
            var token = await _authService.RegisterUser(registerModel);
            return Ok(new { token });
        }
    }

}
