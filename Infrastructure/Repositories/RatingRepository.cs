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
    public class RatingRepository:IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext Context)
        {
            _context = Context;
        }
            public async Task<Rating> AddAsync(Rating rating)
        {
            await _context.Rating.AddAsync(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<IEnumerable<Rating>> GetRatingsForGuideAsync(Guid guideId)
        {
            return await _context.Rating
                .Include(r => r.Guide)
                .Where(r => r.GuideId == guideId)
                .ToListAsync();
        }


        public async Task<IEnumerable<Rating>> GetRatingsForPlaceAsync(Guid placeId)
        {
            return await _context.Rating
                .Include (r => r.Place)
                .Where(r => r.PlaceId == placeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetRatingsByUserAsync(Guid userId)
        {
            return await _context.Rating
                .Include(r=>r.Guide)
                .Include(r=>r.Place)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Rating> GetByIdAsync(Guid id)
        {
            return await _context.Rating
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Rating> GetRatingForGuideByUserAsync(Guid guideId, Guid userId)
        {
            return await _context.Rating
                .FirstOrDefaultAsync(r => r.GuideId == guideId && r.UserId == userId);
        }

        public async Task<Rating> GetRatingForPlaceByUserAsync(Guid placeId, Guid userId)
        {
            return await _context.Rating
                .FirstOrDefaultAsync(r => r.PlaceId == placeId && r.UserId == userId);
        }
        public async Task<bool> DeleteAsync(Rating rating)
        {
            _context.Rating.Remove(rating);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
