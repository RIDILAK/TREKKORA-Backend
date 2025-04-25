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
    public interface ICountryServices
    {
        Task<Responses<string>> AddCountryAsync(CountryDto country);
        Task<Responses<List<GetContryDto>>> GetAllCountriesAsync();

        Task<Responses<GetContryDto>> GetCountryByIdAsync(Guid id);

        Task<Responses<string>> UpdateCountryAsync(Guid id, CountryDto country);

        Task <Responses<string>> DeleteCountryAsync(Guid id);
    }
    public class CountryServices : ICountryServices
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountryServices(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<Responses<string>> AddCountryAsync(CountryDto country)
        {
            //var exist= await _countryRepository.GetByNameAsync(country.CountryName);
            //if (exist == null)
            //{
            //    return new Responses<string> { Message = "Country Alredy exist", StatuseCode = 400 };
            //}
            var countrys = _mapper.Map<Countries>(country);
            await _countryRepository.AddAsync(countrys);
            return new Responses<string> { Message = "Country Added Succesfully", StatuseCode = 200 };



        }

        public async Task<Responses<List<GetContryDto>>> GetAllCountriesAsync()
        {
            var countries = await _countryRepository.GetAllAsync();
            var data = _mapper.Map<List<GetContryDto>>(countries);
            return new Responses<List<GetContryDto>> { Data = data, Message = "Countries fetched", StatuseCode = 200 };
        }

        public async Task<Responses<GetContryDto>> GetCountryByIdAsync(Guid id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country == null)
            {
                return new Responses<GetContryDto> { Message = "Country not Found", StatuseCode = 200 };

            }
            var mappedUser = _mapper.Map<GetContryDto>(country);
            return new Responses<GetContryDto> { Message = "Fetched Country", StatuseCode = 200, Data = mappedUser };
        }

        public async Task<Responses<string>> UpdateCountryAsync(Guid id, CountryDto country)
        {
            var countrys = await _countryRepository.GetByIdAsync(id);
            if (country == null)
            {
                return new Responses<string> { Message = "Country not Found", StatuseCode = 200 };


            }
            countrys.CountryName = country.CountryName;
            countrys.CountryCode = country.CountryCode;
            await _countryRepository.UpdateAsync(countrys);
            return new Responses<string> { Message = "Country Updated Succesfully", StatuseCode = 200 };


        }

        public async Task<Responses<string>> DeleteCountryAsync(Guid id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country == null)
            {
                return new Responses<string> { Message = "Country Not Found", StatuseCode = 401 };

            }
            country.isDeleted = true;
            await _countryRepository.UpdateAsync(country);
            return new Responses<string> { Message = "Country Deleted Successfully", StatuseCode = 200 };
        }
    }
}
