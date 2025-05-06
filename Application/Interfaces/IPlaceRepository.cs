using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPlaceRepository
    {
        Task AddPlaceAsync(Place place, byte[] imageData, string fileName, string contentType);
        Task<List<Place>> GetAllActivePlacesAsync();
        Task<Place> GetPlaceByIdAsync(Guid id);
        Task<List<Place>> GetPlacesByStateIdAsync(Guid stateId);
        Task<List<Place>> GetPlacesByCountryIdAsync(Guid countryId);
        Task UpdatePlaceAsync(Place place);
        Task DeletePlaceAsync(Guid id);
        
    }
}
