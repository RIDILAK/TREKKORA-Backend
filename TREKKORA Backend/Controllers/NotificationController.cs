using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationServices _notificationServices;

        public NotificationController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        [HttpPost("AddNotification")]
        [Authorize]

        public async Task<IActionResult> AddNotification([FromBody]NotificationDto notoficationDto)
        {
            var result = await _notificationServices.SendNotification(notoficationDto.SenderId,notoficationDto.ReceiverId,notoficationDto.Message);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpGet("gettAll")]
        [Authorize]
        public async Task<IActionResult>GetAllNotification(Guid receieverId)
        {
            var result= await _notificationServices.GetNotifications(receieverId);
            return StatusCode(result.StatuseCode,result); 
        }

        [HttpPut("Read/{id}")]
        [Authorize]

        public async Task<IActionResult>ReadNotifications(Guid id)
        {
            var result= await _notificationServices.MarkAsRead(id);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpDelete("Delete")]
        [Authorize]

        public async Task<IActionResult>DeleteNotification(Guid id)
        {
            var result= await _notificationServices.DeleteNotification(id);
            return StatusCode(result.StatuseCode, result);
        }
    }
}
