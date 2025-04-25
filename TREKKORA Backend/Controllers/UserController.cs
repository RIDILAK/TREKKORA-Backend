using System.Security.Claims;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("Get-All")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> GetAllUsers()
        {
            var result= await _userServices.GetAllUsersAsync();
            return StatusCode(result.StatuseCode, result);
        }
        [HttpGet("userById")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>GetUserById(Guid id)
        {
            var result= await _userServices.GetById(id);
            return StatusCode(result.StatuseCode,result);
        }
        [HttpDelete("DeleteUser")]
        [Authorize(Roles ="User")]

        public async Task<IActionResult>DeleteUser() { 

        var userId= User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result= await _userServices.DeleteUser(Guid.Parse(userId));
            return StatusCode(result.StatuseCode,result);
        }

        [HttpPatch("Block/{id}")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> BlockUser(Guid id)
        {
           
            var isBlocked = await _userServices.BlockUser(id);
            return StatusCode(isBlocked.StatuseCode,isBlocked);

        }
    }


}
