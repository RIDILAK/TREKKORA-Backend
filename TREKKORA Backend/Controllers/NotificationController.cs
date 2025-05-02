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

        public async Task<IActionResult> AddNotification([FromBody]NotoficationDto notoficationDto)
        {
            var result = await _notificationServices.SendNotification(notoficationDto.SenderId,notoficationDto.ReceiverId,notoficationDto.Message);
            return StatusCode(result.StatuseCode, result);
        }
    }
}
