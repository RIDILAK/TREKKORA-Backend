using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task SetAvailabiltyFalse(Guid guideId);
        Task SetAvailabilityTrue(Guid guideId);
        Task<List<Booking>> GetAllAsync();
        Task<List<Booking>> GetAllUserBooking(Guid UserId);
        Task<List<Booking>>GetAllGuideBooking(Guid GuideId);
       
        Task<Booking> GetByIdAsync(Guid bookingId);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Guid bookingId);
        Task<List<Booking>>GetByIdPlaceAsync(Guid placeId);

        Task<List<Booking>> GetPendingRequest(Guid guideId);
         Task<List<Booking>> GetApprovedRequest(Guid guideId);
        Task<List<Booking>> GetRejectedRequest(Guid guideId);
        Task<List<Booking>> GetCompletedRequest(Guid guideId);
        Task<Booking> GetByPlace(Guid placeId);
    }
    
}
