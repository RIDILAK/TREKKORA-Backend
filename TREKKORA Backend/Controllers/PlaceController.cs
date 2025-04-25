using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceServices _placeServices;

        public PlaceController(IPlaceServices placeServices)
        {
            _placeServices = placeServices;
        }

        [HttpGet("GettAll")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result=await _placeServices.GetAllPlaces();
            return StatusCode(result.StatuseCode, result);
        }
        [HttpPost("AddPlace")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>AddPlaces([FromForm] AddPlaceDto place,IFormFile image)
        {
            var result= await _placeServices.AddPlaceAsync(place,image);
            return StatusCode(result.StatuseCode,result);
        }
        [HttpGet("GetById")]
        [Authorize(Roles ="Admin")]

        public async Task <IActionResult>GetById(Guid id)
        {
            var result = await _placeServices.GetByIdPlace(id);
            return StatusCode(result.StatuseCode, result);
        }
        //      public async Task<IActionResult> GetWeather(Guid placeId)
        //      {
        //          var result = await _placeServices.GetWeatherByPlaceIdAsync(placeId);
        //          return StatusCode(result.StatuseCode, result);
        //}

        [HttpGet("GetByStates")]
        [Authorize]

        public async Task<IActionResult>GetByState(Guid stateId)
        {
            var result=await _placeServices.GetPlacesByStateIdAsync(stateId);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpGet("GetByCountry")]
        [Authorize]

        public async Task<IActionResult> GetByCountry(Guid countryId)
        {
            var result = await _placeServices.GetPlacesByCountryIdAsync(countryId);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpPut("Update-Place")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>UpdatePlace(Guid id,AddPlaceDto place,IFormFile image)
        {
            var result = await _placeServices.UpdatePlaceAsync(id, place, image);
            return StatusCode(result.StatuseCode,result);
        }
        [HttpDelete("Delete")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>DeleteProduct(Guid id)
        {
            var result = await _placeServices.DeletePlace(id);
            return StatusCode(result.StatuseCode, result);
        }
    }
}
