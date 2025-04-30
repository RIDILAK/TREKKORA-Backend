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
    public interface IUserServices
    {
        Task<Responses<List<GetAllUserDto>>> GetAllUsersAsync();

        Task<Responses<GetAllUserDto>> GetById(Guid id);

        Task<Responses<string>>DeleteUser(Guid id);

        Task<Responses<string>>BlockUser(Guid id);
    }
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Responses<List<GetAllUserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var mappedUsers = _mapper.Map<List<GetAllUserDto>>(users);
            return new Responses<List<GetAllUserDto>> { Data = mappedUsers, Message = "Users retrived Succesfully", StatuseCode = 200 };
        }

        public async Task<Responses<GetAllUserDto>> GetById(Guid id)
        {

            var users = await _userRepository.GetByIdAsync(id);
            if (users == null)
            {
                return new Responses<GetAllUserDto> { Message = "User not found", StatuseCode = 401 };
            }
            var mappedUsers = _mapper.Map<GetAllUserDto>(users);
            return new Responses<GetAllUserDto> { Data = mappedUsers, Message = "User Retrived Succesfully", StatuseCode = 200 };
        }

        public async Task<Responses<string>> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new Responses<string> { Message = "User Not Found", StatuseCode = 403 };

            }
            await _userRepository.DeleteAsync(user);
            return new Responses<string> { Message = "User Deleted Successfully", StatuseCode = 200 };
        }

        public async Task<Responses<string>> BlockUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new Responses<string> { Message = "User not Found", StatuseCode = 404 };
            }

            if (user.Role != null && user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return new Responses<string> { Message = "Admins cannot be blocked", StatuseCode = 403 };
            }

            await _userRepository.BlockUser(user);
            var status = user.IsBlocked ? "Blocked" : "Unblocked";
            return new Responses<string> { StatuseCode = 200, Message = $"User {status} Succesfully" };
        }
    }
    
}
