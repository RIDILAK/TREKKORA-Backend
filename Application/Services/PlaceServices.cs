using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services
{
    public interface IPlaceServices
    {
        Task<Responses<List<PlaceDto>>> GetAllPlaces();
        Task<Responses<string>> AddPlaceAsync(AddPlaceDto placeDto, IFormFile image);
        Task<Responses<PlaceDto>> GetByIdPlace(Guid id);
        Task<Responses<WeatherResponseDto>> GetWeatherByPlaceIdAsync(Guid placeId);

        Task<Responses<List<PlaceDto>>> GetPlacesByStateIdAsync(Guid stateId);
        Task<Responses<List<PlaceDto>>> GetPlacesByCountryIdAsync(Guid countryId);
        Task <Responses<string>> UpdatePlaceAsync(Guid id, AddPlaceDto updatedPlace, IFormFile image);
        Task<Responses<string>>DeletePlace(Guid id);
        Task<byte[]> ConvertToBytes(IFormFile image);
    }
    public class PlaceServices:IPlaceServices
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IStatesRepository _stateRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICLoudinaryServices _cloudinaryRepository;
        private readonly IWeatherServices _weatherRepository;
        private readonly IMapper _mapper;

        public PlaceServices(IPlaceRepository placeRepository,
            IStatesRepository statesRepository,
            ICLoudinaryServices cLoudinaryServices,
            IWeatherServices weather,
            IMapper mapper)
        {
            _placeRepository = placeRepository;
            _stateRepository = statesRepository;
            _cloudinaryRepository = cLoudinaryServices;
            _weatherRepository = weather;
            _mapper = mapper;
        }

        public async Task<Responses<List<PlaceDto>>> GetAllPlaces()
        {
            var places = await _placeRepository.GetAllActivePlacesAsync();
            var result = _mapper.Map<List<PlaceDto>>(places);
            return new Responses<List<PlaceDto>>()
            {
                Data = result,
                Message = "Places fetched successfully",
                StatuseCode = 200
            };
        }

        public async Task<Responses<PlaceDto>> GetByIdPlace(Guid id)
        {
            var places = await _placeRepository.GetPlaceByIdAsync(id);
            if (places == null)
            {
                return new Responses<PlaceDto>() { Message = "Id not Found", StatuseCode = 400 };

            }
            var result= _mapper.Map<PlaceDto>(places);
            return new Responses<PlaceDto> { Data= result,Message="Place Fetched",StatuseCode=200 };
        }

       public async Task<byte[]> ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }


        public async Task<Responses<string>> AddPlaceAsync(AddPlaceDto placeDto, IFormFile image)
        {
            //var imageUrl = await _cloudinaryRepository.UploadImage(image);
            var place = _mapper.Map<Place>(placeDto);
            //place.ImageUrl = imageUrl;
            var imageData=await ConvertToBytes(image);
            if (place.Images == null)
            {
                place.Images = new Images(); // Make sure the Images property is not null
            }
            place.ImageUrl = Convert.ToBase64String(imageData.ToArray()); 
            place.Id = Guid.NewGuid();
            place.CreatedAt = DateTime.UtcNow;

            await _placeRepository.AddPlaceAsync(place,imageData,image.FileName,image.ContentType);
            return new Responses<string> { Message = "Place added successfully", StatuseCode = 200 };
        }

        public async Task<Responses<WeatherResponseDto>> GetWeatherByPlaceIdAsync(Guid placeId)
        {
           
            var place=  await _placeRepository.GetPlaceByIdAsync(placeId);
            var fullpalce = _mapper.Map<PlaceDto>(place);
            var weatherData = await _weatherRepository.GetWeatherByPincodeAsync(fullpalce.Pincode,fullpalce.CountryCode);

            return new Responses<WeatherResponseDto>
            {
                Data = weatherData,
                Message = "Weather fetched successfully",
                StatuseCode = 200
            };
        }

        public async Task<Responses<List<PlaceDto>>> GetPlacesByStateIdAsync(Guid stateId)
        {
           var state= await _placeRepository.GetPlacesByStateIdAsync(stateId);
            var mapped = _mapper.Map<List<PlaceDto>>(state);
            return new Responses<List<PlaceDto>> { StatuseCode = 200,Data=mapped,Message="Places Fetched" };

        }

       public async Task<Responses<List<PlaceDto>>> GetPlacesByCountryIdAsync(Guid countryId)
        {
            var country= await _placeRepository.GetPlacesByCountryIdAsync(countryId);
            var mapped = _mapper.Map<List<PlaceDto>>(country);

            return new Responses<List<PlaceDto>>{ StatuseCode = 200,Message="Place Fetched",Data=mapped};
        }

        public async Task<Responses<string>> UpdatePlaceAsync(Guid id, AddPlaceDto updatedPlace, IFormFile image)
        {
            var existingPlace = await _placeRepository.GetPlaceByIdAsync(id);
            if (existingPlace == null)
            {
                return new Responses<string> { Message = "Id not Found", StatuseCode = 400 };
            }


           
                var imageUrl = await _cloudinaryRepository.UploadImage(image);
                //existingPlace.ImageUrl = imageUrl;
            

            existingPlace.PlaceName = updatedPlace.PlaceName;
            existingPlace.Description = updatedPlace.Description;
            existingPlace.Price = updatedPlace.Price;
            existingPlace.Pincode = updatedPlace.Pincode;
            existingPlace.MinimumDays = updatedPlace.MinimumDays;
            existingPlace.BestTimeToTravel = updatedPlace.BestTimeToTravel;
            

            await _placeRepository.UpdatePlaceAsync(existingPlace);
            return new Responses<string> { StatuseCode = 200, Message = "Place Update Succesfully" };
        }

     public async   Task<Responses<string>> DeletePlace(Guid id)
        {
             await _placeRepository.DeletePlaceAsync(id);
            return new Responses<string> {Message="Place Deleted succsefull",StatuseCode=200};
        }


    }
}
