using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TREKKORA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {

        private readonly IStateServices _stateServices;

        public StateController(IStateServices stateServices)
        {
            _stateServices = stateServices;
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddState(AddStatesDto addStatesDto)
        {
            var result = await _stateServices.AddStates(addStatesDto);
            return StatusCode(result.StatuseCode, result);
        }

        [HttpGet("Get")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> GetAllStates()
        {
            var result= await _stateServices.GetStates();
            return StatusCode(result.StatuseCode,result);
        }

        [HttpGet("GetById")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>GetById(Guid id)
        {
            var result= await _stateServices.GetStatesById(id);
            return StatusCode(result.StatuseCode, result);
        }
        [HttpPut("Update")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>UpdateState(Guid id,UpdateStateDto updateStateDto)
        {
            var result= await _stateServices.UpdateState(id,updateStateDto);
            return StatusCode(result.StatuseCode,result) ;
        }

        [HttpDelete("Delete")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult>DeleteState(Guid id)
        {
            var result=await _stateServices.DeleteState(id);
            return StatusCode(result.StatuseCode, result);
        }
    }
}
