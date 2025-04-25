using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public interface IStateServices
    {
        Task<Responses<string>> AddStates(AddStatesDto dto);
        Task<Responses<IEnumerable<GetStateDto>>> GetStates();

        Task<Responses<GetStateDto>> GetStatesById(Guid id);

        Task<Responses<string>> UpdateState(Guid id, UpdateStateDto dto);

        Task<Responses<string>> DeleteState(Guid id);

    }
    public class StateServices : IStateServices
    {
        private readonly IMapper _mapper;
        private readonly IStatesRepository _statesRepository;

        public StateServices(IMapper mapper, IStatesRepository statesRepository)
        {

            _mapper = mapper;
            _statesRepository = statesRepository;
        }

        public async Task<Responses<string>> AddStates(AddStatesDto dto)
        {
            var states = _mapper.Map<States>(dto);
            await _statesRepository.AddAsync(states);
            return new Responses<string> { Message = "State added Succesfully", StatuseCode = 200 };
        }

        public async Task<Responses<IEnumerable<GetStateDto>>> GetStates()
        {
            var states = await _statesRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<GetStateDto>>(states);
            return new Responses<IEnumerable<GetStateDto>> { Data = result, Message = "States Fetched Succesfully", StatuseCode = 200 };

        }

        public async Task<Responses<GetStateDto>> GetStatesById(Guid id)
        {
            var states = await _statesRepository.GetByIdAsync(id);
            if (states == null)
            {
                return new Responses<GetStateDto> { Message = "State is not found", StatuseCode = 400 };


            }
            var mapped = _mapper.Map<GetStateDto>(states);
            return new Responses<GetStateDto> { Message = "State Fetched", StatuseCode = 200, Data = mapped };


        }
        public async Task<Responses<string>> UpdateState(Guid id, UpdateStateDto dto)
        {
            var states = await _statesRepository.GetByIdAsync(id);
            if (states == null)
            {
                return new Responses<string> { Message = "State is not defined", StatuseCode = 400 };

            }
            states.StateName = dto.StateName;
            await _statesRepository.UpdateAsync(states);
            return new Responses<string> { Message = "Satte Update Succesfully", StatuseCode = 200 };

        }

        public async Task<Responses<string>> DeleteState(Guid id)
        {
            var states = await _statesRepository.GetByIdAsync(id);
            if (states == null)
            {
                return new Responses<string> { Message = "Id not Found", StatuseCode = 400 };
            }
            await _statesRepository.DeleteAsync(states);
            return new Responses<string> { Message = "State deleted Succesfully", StatuseCode = 200 };
        }
    }
}
