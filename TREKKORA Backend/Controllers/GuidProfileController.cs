using System.Security.Claims;
using Application.Dto;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidProfileController : ControllerBase
    {
        private readonly IGuideProfileService _guideProfileService;

        public GuidProfileController(IGuideProfileService guideProfileService)
        {
            _guideProfileService = guideProfileService;
        }

        [HttpGet("All")]
        [Authorize(Roles ="Admin,User")]

        public async Task<IActionResult> GetAll()
        {
            var result=await _guideProfileService.GetAllGuides();
            return StatusCode(result.StatuseCode, result);
        }

        [HttpGet("GetById")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>GetByIdGuid(Guid id)
        {
            
            var result= await _guideProfileService.GetByIdGuides(id);
            return StatusCode(result.StatuseCode,result);
        }
        [HttpPost("Add")]
        [Authorize(Roles ="Guide")]

        public async Task<IActionResult> AddGuide([FromForm]  GuideProfileDto guideProfile, IFormFile image, IFormFile image2)
        {
            var guideId=User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value;
            var reult=await _guideProfileService.AddProfile( guideProfile, image, image2,(Guid.Parse(guideId)));
            return StatusCode(reult.StatuseCode,reult);
        }

        [HttpPut("Update")]
        [Authorize(Roles ="Guide")]

        public async Task<IActionResult> UpdateGuide([FromForm] GuideDto guideProfile, IFormFile image,IFormFile image2)
        {
            var guideId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _guideProfileService.UpdateProfile((Guid.Parse(guideId)),guideProfile, image, image2);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles ="Guide")]

        public async Task<IActionResult>DeleteGuide()
        {
            var guideId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _guideProfileService.DeleteProfile((Guid.Parse(guideId)));
            return StatusCode(result.StatuseCode,result);
        }

        [HttpPatch("Block")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>BlockUser(Guid id)
        {
            var result= await _guideProfileService.ToggleBlockGuide(id);
            return StatusCode(result.StatuseCode, result);
        }

    }
}
