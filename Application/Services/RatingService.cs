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

   public interface IRatinServices
    {
        Task<Responses<Rating>> CreateRatingForGuideAsync(RatingGuideDto dto,Guid userId);
        Task<Responses<Rating>> CreateRatingForPlaceAsync(CreateRatingForPlaceDto dto,Guid userId);
        Task<Responses<IEnumerable<GetRatingGuideDto>>> GetRatingsForGuideAsync(Guid guideId);
        Task<Responses<IEnumerable<GetRatingPlaceDto>>> GetRatingsForPlaceAsync(Guid placeId);
        Task<Responses<IEnumerable<GetRatingByUser>>> GetRatingsByUserAsync(Guid userId);
        Task<Responses<bool>> DeleteRatingAsync(Guid id);
    }
    public class RatingService:IRatinServices   
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository,IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }
        public async Task<Responses<Rating>> CreateRatingForGuideAsync(RatingGuideDto dto, Guid userId)
        {
            var existingRating = await _ratingRepository.GetRatingForGuideByUserAsync(dto.GuideId, userId);

            if (existingRating != null)
            {
                return new Responses<Rating>
                {
                    StatuseCode = 400,
                    Message = "You have already rated this Guide"
                };
            }
            var rating = _mapper.Map<Rating>(dto);
            rating.UserId = userId;
          var result=   await _ratingRepository.AddAsync(rating);
            return new Responses<Rating> { Data = result,StatuseCode=200,Message="Rated Succsefully" };

        }

        public async Task<Responses<Rating>> CreateRatingForPlaceAsync(CreateRatingForPlaceDto dto, Guid userId)
        {
            var existingRating = await _ratingRepository.GetRatingForPlaceByUserAsync(dto.PlaceId, userId);

            if (existingRating != null)
            {
                return new Responses<Rating>
                {
                    StatuseCode = 400,
                    Message = "You have already rated this Place"
                };
            }

            var rating = await _ratingRepository.GetRatingsByUserAsync(userId);

            if (rating == null)
            {
                return new Responses<Rating> { Message = "User not found", StatuseCode = 400 };
            }

            var mapped = _mapper.Map<Rating>(dto);
            mapped.UserId = userId;
            var result = await _ratingRepository.AddAsync(mapped);

            return new Responses<Rating> { StatuseCode = 200, Message = "Reviewed Successfully" };
        }


        public async Task<Responses<IEnumerable<GetRatingGuideDto>>> GetRatingsForGuideAsync(Guid guideId)
        {
           var guide= await _ratingRepository.GetRatingsForGuideAsync(guideId);
            if (guide == null)
            {
                return new Responses<IEnumerable<GetRatingGuideDto>> { StatuseCode = 400, Message = "Guide not Found" };
            }
          var mapped =  _mapper.Map<IEnumerable<GetRatingGuideDto>>(guide);
            return new Responses<IEnumerable<GetRatingGuideDto>> { Message = "Fetched", StatuseCode = 200,Data= mapped };
        }

        public async Task<Responses<IEnumerable<GetRatingPlaceDto>>> GetRatingsForPlaceAsync(Guid placeId)
        {
            var place= await _ratingRepository.GetRatingsForPlaceAsync(placeId);
            if (place == null)
            {
                return new Responses<IEnumerable<GetRatingPlaceDto>> { Message = "Place not found", StatuseCode = 400 };
            }
            var mapped= _mapper.Map<IEnumerable<GetRatingPlaceDto>>(place);
            return new Responses<IEnumerable<GetRatingPlaceDto>> {Message="Fetched",StatuseCode = 200,Data=mapped };
        }

        public async Task<Responses<IEnumerable<GetRatingByUser>>> GetRatingsByUserAsync(Guid userId)
        {
            var user= await _ratingRepository.GetRatingsByUserAsync(userId);
            if (user == null)
            {
                return new Responses<IEnumerable<GetRatingByUser>> { StatuseCode = 400, Message = "User not found" };
                
            }
            var mapped=_mapper.Map<IEnumerable<GetRatingByUser>>(user);
            return new Responses<IEnumerable<GetRatingByUser>> {Message="Fetched",StatuseCode=200,Data= mapped };
        }
        public async Task<Responses<bool>> DeleteRatingAsync(Guid id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);

            if (rating == null)
            {
                return new Responses<bool> { StatuseCode = 400, Message = "Id not Found" };
            }
             await _ratingRepository.DeleteAsync(rating);
                return new Responses<bool> { Message = "Rating Deleted Succesfully", StatuseCode = 200 };
        }
    }
}
