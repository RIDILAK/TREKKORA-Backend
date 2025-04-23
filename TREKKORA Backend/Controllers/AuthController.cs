using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthServices _services;

        public AuthController(IAuthServices services)
        {
            _services = services;
        }
        [HttpPost("Register")]
        public async Task <IActionResult>Register(RegisterDto registerDto)
        {
            var result=await _services.RegisterAsync(registerDto);
            return StatusCode(result.StatuseCode, result);
        }
        [HttpPost("Login")]

        public async Task<IActionResult>Login(LoginDto loginDto)
        {
            var result=await _services.LoginAsync(loginDto);
            return StatusCode(result.StatuseCode,result);
        }
    }
}
