using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRatingRepository
    {

        Task<Rating> AddAsync(Rating rating);
        Task<IEnumerable<Rating>> GetRatingsForGuideAsync(Guid guideId);
        Task<IEnumerable<Rating>> GetRatingsForPlaceAsync(Guid placeId);
        Task<IEnumerable<Rating>> GetRatingsByUserAsync(Guid userId);
        Task<Rating> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Rating rating);
        Task<Rating> GetRatingForGuideByUserAsync(Guid guideId, Guid userId);
        Task<Rating> GetRatingForPlaceByUserAsync(Guid placeId, Guid userId);
    }
}
