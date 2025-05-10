using System.Security.Claims;
using Application.Dto;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingServices _services;
        private readonly IBookingRepository _repository;

        public BookingController(IBookingServices services,IBookingRepository bookingRepository)
        {
            _services = services;
            _repository = bookingRepository;
        }

        [HttpPost("Create")]
        //[Authorize(Roles ="User")]

        public async Task<IActionResult> CreateBooking([FromBody] AddBookingDto dto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result= await _services.AddBookingAsync(dto,Guid.Parse(userId));
            return StatusCode(result.StatuseCode, result);
        }
        [HttpGet("Get-All")]
        //[Authorize]

        public async Task<IActionResult> GetAllBooking()
        {
            var result = await _services.GetAllBooking();
            return StatusCode(result.StatuseCode,result);
        }
        [HttpGet("GetByBookingId")]
        [Authorize]
        public async Task<IActionResult>GetByBooking(Guid bookingId)
        {
            var result= await _services.GetBookingById(bookingId);
            return StatusCode(result.StatuseCode, result);
        }
        [HttpGet("User")]
        //[Authorize(Roles ="User,Admin")]
        [Authorize]

        public async Task<IActionResult>GetBookingUser()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result=await _services.GetAllBookingUser(Guid.Parse(userId));

            return StatusCode(result.StatuseCode,result);
        }
        [HttpGet("GetBookingStatus")]
        public async Task<IActionResult> GetBookingStatus(Guid placeId, Guid guideId, Guid userId)
        {
            var bookings = await _repository.GetAllAsync();
            var booking = bookings.FirstOrDefault(b => b.PlaceId == placeId && b.GuideId == guideId && b.UserId == userId);

            if (booking == null)
                return NotFound(new { message = "No booking found" });

            return Ok(new { status = booking.Status });
        }


        [HttpGet("Guide")]
        [Authorize(Roles ="Guide,Admin")]

        public async Task <IActionResult>GetBookinGuide(Guid guideId)
        {
            var result= await _services.GetAllBookingGuid(guideId);
            return StatusCode(result.StatuseCode, result);
        }
        [HttpGet("Place")]
        //[Authorize(Roles ="Admin")]

        public async Task<IActionResult>GetBookingPlace(Guid placeId)
        {
            var result= await _services.GetAllBookingPlace(placeId);
            return StatusCode(result.StatuseCode,result);
        }

        [HttpGet("Pending=Requests")]
        [Authorize(Roles ="Guide,Admin")]

        public async Task<IActionResult> GetPendingRequest()
        {
            var guideId= User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result= await _services.GetAllPendingBooking(Guid.Parse(guideId));
            return StatusCode(result.StatuseCode, result);
        }

        [HttpPut("Update-Status")]
        //[Authorize(Roles ="Guide")]

        public async Task<IActionResult>UpdateStatus(Guid Id,UpdateBookingStatusDto dto)
        {
            var result=await _services.UpdateBookingStatusAsync(Id, dto);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpPut("Update-Date")]
        [Authorize(Roles ="User")]

        public async Task <IActionResult>UpdateDate(Guid id,UpdateBookingDatesDto dto)
        {
            var result= await _services.UpdateBookingDatesAsync(id, dto);
            return StatusCode(result.StatuseCode,result);
        }

        [HttpDelete("Delete")]
        //[Authorize(Roles ="User")]

        public async Task<IActionResult>DeleteBooking(Guid id)
        {
            var result= await _services.DeleteBookingAsync(id);
            return StatusCode(result.StatuseCode,result);
        }

    }

    
}
