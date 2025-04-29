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
    public interface IBookingServices
    {
        Task<Responses<string>> AddBookingAsync(AddBookingDto dto, Guid userId);
        Task<Responses<List<GetBookingDto>>> GetAllBookingUser(Guid userId);
        Task<Responses<List<GetBookingDto>>> GetAllBookingGuid(Guid guideId);
        Task<Responses<List<GetBookingDto>>> GetAllBookingPlace(Guid placeId);
        Task<Responses<List<GetBookingDto>>> GetAllBooking();
        Task<Responses<string>> UpdateBookingStatusAsync(Guid Id, UpdateBookingStatusDto dto);

        Task<Responses<string>> UpdateBookingDatesAsync(Guid id, UpdateBookingDatesDto dto);

        Task <Responses<string>> DeleteBookingAsync(Guid id);
    }
    public class BookinServices : IBookingServices
    {
        private readonly IBookingRepository _bookingrepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPlaceRepository _placeRepository;

        public BookinServices(IBookingRepository repository, IMapper mapper, IUserRepository userRepository, IPlaceRepository placeRepository)
        {
            _bookingrepository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _placeRepository = placeRepository;

        }

        public async Task<Responses<string>> AddBookingAsync(AddBookingDto dto, Guid userId)
        {
            var place = await _placeRepository.GetPlaceByIdAsync(dto.PlaceId);
            if (place == null)
            {

                return new Responses<string>
                {
                    Message = "Place is Not Found",
                    StatuseCode = 400
                };
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {


                return new Responses<string> { Message = "User Not Found", StatuseCode = 400 };
            }

            var guide = _userRepository.GetByIdAsync(dto.GuideId);
            if (guide == null)
            {

                return new Responses<string> { Message = "Guide Not Found", StatuseCode = 400 };
            }

            int TotalDays = (dto.EndDate - dto.StartDate).Days + 1;

            if (TotalDays < place.MinimumDays)
            {
                return new Responses<string> { Message = $"You Must book atleast{place.MinimumDays} days for the trip." };


            }

            decimal totalPrice = dto.NumberOfPeople * place.Price;

            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                UserId= userId,
                GuideId = dto.GuideId,
                PlaceId = dto.PlaceId,
                NumberOfPeople = dto.NumberOfPeople,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                BookingDate = DateTime.Now,
                TotalPrice = totalPrice,
                Status = "Pending",
                CreatedAt = DateTime.Now,
            };
            await _bookingrepository.AddAsync(booking);
            return new Responses<string> { Message = "Booking Creatd Succesfully", StatuseCode = 200 };




        }

        public async Task<Responses<List<GetBookingDto>>> GetAllBookingUser(Guid userId)
        {
            var booking = await _bookingrepository.GetAllAsync();
            if (booking == null)
            {
                return new Responses<List<GetBookingDto>> { Message = "User Not Found", StatuseCode = 400 };
            }
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { Data = mapped, Message = "Fetched", StatuseCode = 200 };
        }

        public async Task<Responses<List<GetBookingDto>>> GetAllBookingPlace(Guid placeId)
        {
            var booking = await _bookingrepository.GetByIdPlaceAsync(placeId);
            if (booking == null)
            {
                return new Responses<List<GetBookingDto>> { Message = "Place not Found", StatuseCode = 400 };
            }
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { StatuseCode = 200, Message = "Fetched", Data = mapped };
        }

        public async Task<Responses<List<GetBookingDto>>> GetAllBookingGuid(Guid guideId)
        {
            var booking = await _bookingrepository.GetAllGuideBooking(guideId);
            if (booking == null)
            {
                return new Responses<List<GetBookingDto>> { Message = "Guide not Found", StatuseCode = 400 };

            }
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { StatuseCode = 200, Data = mapped, Message = "Fetched" };

        }

        public async Task<Responses<List<GetBookingDto>>> GetAllBooking()
        {
            var booking = await _bookingrepository.GetAllAsync();
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { Message = "Fetched Booking", StatuseCode = 200, Data = mapped };

        }

        public async Task<Responses<string>> UpdateBookingStatusAsync(Guid Id, UpdateBookingStatusDto dto)
        {
            var booking = await _bookingrepository.GetByIdAsync(Id);
            if (booking == null)
            {
                return new Responses<string> { StatuseCode = 400, Message = "Booking Not Found" };
            }

            booking.Status = dto.Status;
            await _bookingrepository.UpdateAsync(booking);
            return new Responses<string> { StatuseCode = 200, Message = "Status Changed" };
        }

        public async Task<Responses<string>> UpdateBookingDatesAsync(Guid id, UpdateBookingDatesDto dto)
        {
            var booking = await _bookingrepository.GetByIdAsync(id);
            if (booking == null)
            {
                return new Responses<string> { Message = "Booking Not Found", StatuseCode = 400 };

            }

            if (!booking.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
            {
                return new Responses<string> { Message = "Booking data can be updated after guide Approval", StatuseCode = 403 };
            }

            if (dto.EndDate < dto.StartDate)
            {
                return new Responses<string> { Message = "EndDate cannot be earlier than startDate" };
            }

            var place = await _placeRepository.GetPlaceByIdAsync(booking.PlaceId);

            if (place == null)
            {
                return new Responses<string> { Message = "Place not Found", StatuseCode = 400 };
            }
            int days = (dto.EndDate.Date - dto.StartDate.Date).Days + 1;
            if (days < booking.place.MinimumDays)
            {
                return new Responses<string> { Message = $"Bookin must be at least {booking.place.MinimumDays} days" };
            }

            booking.StartDate = dto.StartDate;
            booking.EndDate = dto.EndDate;
            await _bookingrepository.UpdateAsync(booking);
            return new Responses<string> { Message = "Date Updated", StatuseCode = 200 };
        }

        public async Task<Responses<string>> DeleteBookingAsync(Guid id)
        {
            var booking = await _bookingrepository.GetByIdAsync(id);
            if (booking == null)
            {
                return new Responses<string> { Message = "Bookin Not Found", StatuseCode = 400 };

            }
            if (!booking.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
            {
                return new Responses<string> { Message = "Booking can only be deleted after guide approval.", StatuseCode = 403 };
            }

            await _bookingrepository.DeleteAsync(booking);
            return new Responses<string> { Message = "Booking Deleted Succesfully", StatuseCode = 200 };
        }
    }
}
