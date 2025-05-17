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
        public decimal CalculateGuideSalary(int numberOfPeople, decimal totalPrice);
        Task<Responses<string>> AddBookingAsync(AddBookingDto dto, Guid userId);
        Task<Responses<List<GetBookingDto>>> GetAllBookingUser(Guid userId);
        Task<Responses<GetBookingDto>>GetBookingById(Guid bookingId);
        Task<Responses<List<GetBookingDto>>> GetAllBookingGuid(Guid guideId);
        Task<Responses<List<GetBookingDto>>> GetAllBookingPlace(Guid placeId);
        Task<Responses<List<GetBookingDto>>> GetAllPendingBooking(Guid guideId);
        Task<Responses<List<GetBookingDto>>> GetAllApprovedBooking(Guid guideId);
        Task<Responses<List<GetBookingDto>>> GetAllRejectedBooking(Guid guideId);
        Task<Responses<List<GetBookingDto>>> GetAllCompletedBooking(Guid guideId);
        Task<Responses<List<GetBookingDto>>> GetAllBooking();
        Task<Responses<string>> UpdateBookingStatusAsync(Guid guideId, Guid Id, UpdateBookingStatusDto dto);

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
        public decimal CalculateGuideSalary(int numberOfPeople, decimal totalPrice)
        {
            decimal percentage = numberOfPeople switch
            {
                1 => 0.30m,
                2 => 0.25m,
                3 => 0.20m,
                4 => 0.18m,
                5 => 0.16m,
                6 => 0.14m,
                7 => 0.12m,
                8 => 0.10m,
                9 => 0.08m,
                10 => 0.05m,
                _ => 0.00m
            };

            return totalPrice * percentage;
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

            var guide =await _userRepository.GetByIdAsync(dto.GuideId);
            
            if (guide == null)
            {

                return new Responses<string> { Message = "Guide Not Found", StatuseCode = 400 };
            }

            int TotalDays = (dto.EndDate - dto.StartDate).Days + 1;

            if (TotalDays < place.MinimumDays)
            {
                return new Responses<string> { StatuseCode=400, Message = $"You Must book atleast{place.MinimumDays} days for the trip." };


            }
            if (dto.NumberOfPeople > 10)
            {
                return new Responses<string>
                {
                    StatuseCode = 405,
                    Message = "You can book a maximum of 10 people per trip."
                };
            }

            decimal totalPrice = dto.NumberOfPeople * place.Price * TotalDays;
            decimal guideSalary = CalculateGuideSalary(dto.NumberOfPeople, totalPrice);

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
                GuideSalary = guideSalary,
                Status = "Pending",
                CreatedAt = DateTime.Now,
            };
            await _bookingrepository.AddAsync(booking);
            //guide.GuideProfile.isAvailable=false;
            
            return new Responses<string> { Message = "Booking Created Succesfully", StatuseCode = 200,Data=booking.BookingId.ToString() };




        }

        public async Task<Responses<List<GetBookingDto>>> GetAllBookingUser(Guid userId)
        {
            var booking = await _bookingrepository.GetAllUserBooking(userId);
            if (booking == null)
            {
                return new Responses<List<GetBookingDto>> { Message = "User Not Found", StatuseCode = 400 };
            }
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>>{ Data = mapped, Message = "Fetched", StatuseCode = 200 };
        }
      public  async Task<Responses<GetBookingDto>> GetBookingById(Guid bookingId)
        {
            var booking = await _bookingrepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return new Responses<GetBookingDto> { Message = "Booking Id Not Found", StatuseCode = 400 };
            }
            var mapped = _mapper.Map<GetBookingDto>(booking);
            return new Responses<GetBookingDto> { Data = mapped, Message = "Fetched", StatuseCode = 200 };
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

      public async  Task<Responses<List<GetBookingDto>>> GetAllPendingBooking(Guid guideId)
        {
            var booking= await _bookingrepository.GetPendingRequest(guideId);
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { Data = mapped, StatuseCode = 200, Message = "Pending Booking Fetched" };

        }

        public async Task<Responses<List<GetBookingDto>>> GetAllApprovedBooking(Guid guideId)
        {
            var booking = await _bookingrepository.GetApprovedRequest(guideId);
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { Data = mapped, StatuseCode = 200, Message = "Approved Booking Fetched" };

        }
        public async Task<Responses<List<GetBookingDto>>> GetAllRejectedBooking(Guid guideId)
        {
            var booking = await _bookingrepository.GetRejectedRequest(guideId);
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { Data = mapped, StatuseCode = 200, Message = "Rejected Booking Fetched" };

        }

        public async Task<Responses<List<GetBookingDto>>> GetAllCompletedBooking(Guid guideId)
        {
            var booking = await _bookingrepository.GetCompletedRequest(guideId);
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { Data = mapped, StatuseCode = 200, Message = "Completed Booking Fetched" };

        }

        public async Task<Responses<List<GetBookingDto>>> GetAllBooking()
        {
            var booking = await _bookingrepository.GetAllAsync();
            var mapped = _mapper.Map<List<GetBookingDto>>(booking);
            return new Responses<List<GetBookingDto>> { Message = "Fetched Booking", StatuseCode = 200, Data = mapped };

        }

        public async Task<Responses<string>> UpdateBookingStatusAsync( Guid guideId, Guid Id, UpdateBookingStatusDto dto)
        {
            var booking = await _bookingrepository.GetByIdAsync(Id);
            if (booking == null)
            {
                return new Responses<string> { StatuseCode = 400, Message = "Booking Not Found" };
            }

            booking.Status = dto.Status;
            await _bookingrepository.UpdateAsync(booking);
            if (dto.Status == "Approved")
            {
                await _bookingrepository.SetAvailabiltyFalse(guideId);
            }

            if(dto.Status == "Completed")
            {
                await _bookingrepository.SetAvailabilityTrue(guideId);
            }
            
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
            if (booking.isDeleted)
            {
                return new Responses<string>
                {
                    Message = "Booking has already been cancelled.",
                    StatuseCode = 409 
                };
            }
            if (!booking.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase) &&
                !booking.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            {
                return new Responses<string>
                {
                    Message = "Booking can only be deleted if it's Approved or Pending.",
                    StatuseCode = 403
                };
            }

            await _bookingrepository.DeleteAsync(id);
            return new Responses<string> { Message = "Booking Deleted Succesfully", StatuseCode = 200 };
        }
    }
}
