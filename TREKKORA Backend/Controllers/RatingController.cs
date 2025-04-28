using System.Security.Claims;
using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatinServices _ratinServices;

        public RatingController(IRatinServices ratinServices)
        {
            _ratinServices = ratinServices;
        }

        [HttpPost("Guide")]
        [Authorize(Roles ="User")]

        public async Task <IActionResult>CreateRatingGuide(RatingGuideDto ratingGuideDto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var rating= await _ratinServices.CreateRatingForGuideAsync(ratingGuideDto,Guid.Parse(userId));
            return StatusCode(rating.StatuseCode, rating);
        }

        [HttpPost("Place")]
        [Authorize(Roles ="User")]

        public async Task<IActionResult>CreateRatingPlace(CreateRatingForPlaceDto createRatingForPlaceDto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var rating= await _ratinServices.CreateRatingForPlaceAsync(createRatingForPlaceDto,Guid.Parse(userId));
            return StatusCode(rating.StatuseCode,rating);
        }

        [HttpGet("Guide")]
        [Authorize]

        public async Task<IActionResult>GetRatingForGuide(Guid GuidId)
        {
            var rating=await _ratinServices.GetRatingsForGuideAsync(GuidId);
            return StatusCode(rating.StatuseCode, rating);
        }

        [HttpGet("Place")]
        [Authorize]

        public async Task<IActionResult>GetRatingForPlace(Guid PlaceId)
        {
            var rating=await _ratinServices.GetRatingsForPlaceAsync(PlaceId);
            return StatusCode(rating.StatuseCode, rating);
        }

        [HttpGet("User")]
        [Authorize]

        public async Task<IActionResult>GetRatingByUser(Guid UserId)
        {
            var rating =await _ratinServices.GetRatingsByUserAsync(UserId);
            return StatusCode(rating.StatuseCode, rating);
        }

        [HttpDelete]
        [Authorize(Roles =("Admin,User"))]

        public async Task<IActionResult>DeleteRating(Guid RatingId)
        {
            var rating= await _ratinServices.DeleteRatingAsync(RatingId);
            return StatusCode(rating.StatuseCode,rating);
        }
    }
}
