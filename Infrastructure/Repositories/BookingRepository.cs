using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Booking booking)
        {
            await _appDbContext.AddAsync(booking);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _appDbContext.Bookings
                .Include(b=>b.place)
                .Include(b=>b.User)
                .Include(b=>b.Guide)
                .ToListAsync();

        }

        public async Task<Booking> GetByIdAsync(Guid bookingId)
        {

            return await _appDbContext.Bookings
                 .Include(b=>b.place)
                 .Include(b=>b.User)
                 .Include(b=>b.Guide)
                .FirstOrDefaultAsync(x => x.BookingId == bookingId);

        }

        public async Task<List<Booking>> GetAllUserBooking(Guid userId)
        {
            return await _appDbContext.Bookings
                .Include(b=>b.place)
                .Include(b=>b.User)
                .Include(b=>b.Guide)
                .Where(b=>b.UserId == userId)
                .ToListAsync();

        }

        public async Task<List<Booking>> GetAllGuideBooking(Guid guideId)
        {
            return await _appDbContext.Bookings
                .Include(b=>b.User)
                .Include(b=>b.Guide)
                .Include(b=>b.place)
                .Where(b=>b.GuideId == guideId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _appDbContext.Bookings.Update(booking);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Booking booking)
        {
            _appDbContext.Bookings.Remove(booking);
            await _appDbContext.SaveChangesAsync();

        }
        public async Task <List<Booking>> GetByIdPlaceAsync(Guid placeId)
        {
            return await _appDbContext.Bookings
                .Include(b=>b.User)
                .Include(b=>b.Guide)
                .Include(b=>b.place)
                .Where(c => c.PlaceId == placeId)
                .ToListAsync();
        }
    }
}

