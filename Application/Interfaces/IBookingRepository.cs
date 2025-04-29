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
        Task<List<Booking>> GetAllAsync();
        Task<List<Booking>> GetAllUserBooking(Guid UserId);
        Task<List<Booking>>GetAllGuideBooking(Guid GuideId);
       
        Task<Booking> GetByIdAsync(Guid bookingId);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Booking booking);
        Task<List<Booking>>GetByIdPlaceAsync(Guid placeId);
    }
    
}
