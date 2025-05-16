using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Application.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User,LoginDto>().ReverseMap();
            CreateMap<User,GetAllUserDto>().ReverseMap();
            CreateMap<Countries,CountryDto>().ReverseMap();
            CreateMap<Countries,GetContryDto>().ReverseMap();
            CreateMap<States,AddStatesDto>().ReverseMap();
            CreateMap<AddPlaceDto, Place>();
            CreateMap<States,UpdateStateDto>().ReverseMap();
            CreateMap<Place, PlaceDto>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId.ToString()))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.StateName))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.State.Countries.CountryName))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.State.Countries.CountryCode))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                    src.Images.ImageData
                ));


            CreateMap<AddPlaceDto, Place>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => Guid.Parse(src.StateId)));
            CreateMap<States,GetStateDto>()
                .ForMember(dest=>dest.CountryName,opt=>opt.MapFrom(src=>src.Countries.CountryName));

            CreateMap<User, GuideDto>()
                .ForMember(dest => dest.GuideProfileDto, opt => opt.MapFrom(src => src.GuideProfile)); ;

            CreateMap<User, GetGuideDto>()
                .ForMember(dest => dest.GetGuideProfileDto, opt => opt.MapFrom(src => src.GuideProfile));


            CreateMap<GuideProfile, GuideProfileDto>().ReverseMap();
            

            CreateMap<GuideProfile,GetGuideProfileDto>()
                .ForMember(dest => dest.PlaceName,opt=>opt.MapFrom(src=>src.Place.PlaceName));
            CreateMap<GetGuideProfileDto, GuideProfile>();


            CreateMap<RatingGuideDto, Rating>();


            CreateMap<CreateRatingForPlaceDto, Rating>();

            CreateMap<Rating,GetRatingGuideDto>()
                .ForMember(dest=>dest.GuideName, opt=>opt.MapFrom(src=>src.Guide.Name))
                .ForMember(dest=>dest.UserName,opt=>opt.MapFrom(src=>src.User.Name));
            CreateMap<Rating,GetRatingPlaceDto>()
                .ForMember(dest=>dest.PlaceName,opt=>opt.MapFrom(src=>src.Place.PlaceName));
            CreateMap<Rating,GetRatingByUser>()
                .ForMember(dest=>dest.GuideName,opt=>opt.MapFrom(src=>src.User.Name))
                .ForMember(dest=>dest.PlaceName,opt=>opt.MapFrom(src=>src.Place.PlaceName));

            CreateMap<Booking,AddBookingDto>().ReverseMap() ;
            CreateMap<Booking, GetBookingDto>()
                .ForMember(dest=>dest.UserName,opt=>opt.MapFrom(src=>src.User.Name))
                .ForMember(dest => dest.PlaceName, opt => opt.MapFrom(src=>src.place.PlaceName))
                .ForMember(dest=>dest.PlaceImage,opt=>opt.MapFrom(src=>src.place.ImageUrl))
                .ForMember(dest=>dest.GuideName,opt=>opt.MapFrom(src=>src.Guide.Name))
                .ForMember(dest=>dest.GuideImage,opt=>opt.MapFrom(src=>src.Guide.GuideProfile.ProfileImage))
                .ReverseMap();
            CreateMap<Booking, UpdateBookingDatesDto>().ReverseMap();
            CreateMap<Booking,UpdateBookingStatusDto>().ReverseMap();
            CreateMap<Message, MessageCreateDto>().ReverseMap();
            CreateMap<Message,MessageResponseDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>();
        }
    }
}
