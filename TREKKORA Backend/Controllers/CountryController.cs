using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices _countryServices;

        public CountryController(ICountryServices countryServices) { 
        
            _countryServices = countryServices;
        }

        [HttpPost("Add")]
        [Authorize(Roles ="Admin")]

        public async Task <IActionResult> AddCountry(CountryDto countryDto)
        {
            var result= await _countryServices.AddCountryAsync(countryDto);
            return StatusCode(result.StatuseCode, result);
        }
        [HttpGet("All")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> GetAll()
        {
            var result= await _countryServices.GetAllCountriesAsync();
            return StatusCode(result.StatuseCode,result);
        }

        [HttpGet("ById")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>GetById(Guid id)
        {
            var result=await _countryServices.GetCountryByIdAsync(id);
            return StatusCode(result.StatuseCode,result );
        }

        [HttpPut("Add")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>UpdateCountry(Guid id, CountryDto countryDto)
        {
            var result=await _countryServices.UpdateCountryAsync(id, countryDto);
            return StatusCode (result.StatuseCode,result);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>DeleteCountry(Guid id)
        {
            var result=await _countryServices.DeleteCountryAsync(id);
            return StatusCode (result.StatuseCode,result);
        }

       


    }

}
