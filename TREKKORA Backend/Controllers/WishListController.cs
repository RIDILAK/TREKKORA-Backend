using System.Security.Claims;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {

        private readonly IWishListServices _services;

        public WishListController(IWishListServices services)
        {
            _services = services;
        }

        [HttpPost("Add-or-Remove")]
        [Authorize(Roles ="User")]

        public async Task <IActionResult>AddOrRemoveWishList(Guid placeId)
        {
           var userId= User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result= await _services.AddOrRemoveWishList(placeId,Guid.Parse(userId));
            return StatusCode(result.StatuseCode, result);
        }

        [HttpGet("GetAll")]
        [Authorize]

        public async Task<IActionResult> GetWishList()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result= await _services.GetWishList(Guid.Parse(userId));
            return StatusCode(result.StatuseCode, result);
        }
    }
}
