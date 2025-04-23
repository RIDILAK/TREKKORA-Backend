using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using Trekkora.Application.Interfaces;

namespace Application.Services
{
    public interface IAuthServices
    {
        Task<Responses<string>>RegisterAsync(RegisterDto registerDto);
        Task<Responses<string>>LoginAsync(LoginDto loginDto);
    }
    public class AuthServices:IAuthServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtServices _jwtServices;
        private readonly IMapper _mapper;

        public AuthServices(IUserRepository userRepository, IJwtServices jwtServices, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtServices = jwtServices;
            _mapper = mapper;
        }
        public async Task<Responses<string>> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {

                return new Responses<string> { Message = "Email Already Exist", StatuseCode = 409 };
            }
            registerDto.Password = HashPassword(registerDto.Password);
            var user = _mapper.Map<User>(registerDto);
            

            
                await _userRepository.AddAsync(user);
                return new Responses<string> { Message = "Registration Succesfully Completed", StatuseCode = 200 };

            }
            catch (Exception ex)
            {
                return new Responses<string> { Message = "Something Went wrong:" + ex.Message, StatuseCode = 500 };
            };

        }

        public async Task<Responses<string>> LoginAsync(LoginDto loginDto)
        {
            try
            {

                var user = await _userRepository.GetByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return new Responses<string> { Message = "Invalid User", StatuseCode = 400 };
                }
                if (user.IsBlocked)
                {
                    return new Responses<string> { Message = "You are blocked by Admin", StatuseCode = 403 };
                }

                var verifiedPassword = VerifiedPassword(loginDto.Password, user.Password);

                if (!verifiedPassword)
                {
                    return new Responses<string> { Message = "Invalid Password", StatuseCode = 401 };
                }

                var token = _jwtServices.GenerateToken(user);

                return new Responses<string> { Data = token, Message = "Login Succesfully", StatuseCode = 200 };
            }
            catch (Exception ex) {

                return new Responses<string> { Message = $"An Error Occured while processing your Request:{ex.Message}", StatuseCode = 500 };
            
            }
        }
            



        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifiedPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
