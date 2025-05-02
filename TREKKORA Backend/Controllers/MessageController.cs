using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageServices _services;

        public MessageController(IMessageServices services)
        {
            _services = services;
        }

        [HttpPost("Add-Message")]
        [Authorize]

        public async Task<IActionResult> AddMessage([FromBody] MessageCreateDto messagrCreateDto)
        {
            var result= await _services.AddMessageAsync(messagrCreateDto);
            return StatusCode(result.StatuseCode,result);
        }

        [HttpGet("All")]
        [Authorize]
        public async Task<IActionResult>GetMessage(Guid senderId,Guid receiverId)
        {
            var result= await _services.GetMessagesAsync(senderId,receiverId);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpPatch("Read")]
        [Authorize]

        public async Task<IActionResult>MarkAsRead(Guid id)
        {
            var result= await _services.MarkAsRead(id);
            return StatusCode(result.StatuseCode, result);
        }
    }
}
