using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PlaceRepository:IPlaceRepository
    {
        private readonly AppDbContext _appDbContext;

        public PlaceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
       public async Task AddPlaceAsync(Place place)
        {
            await _appDbContext.Places.AddAsync(place);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<List<Place>> GetAllActivePlacesAsync()
        {
            return await _appDbContext.Places
                .Include(p => p.State)
                .ThenInclude(s=>s.Countries)
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Place> GetPlaceByIdAsync(Guid id)
        {
            return await _appDbContext.Places
                .Include(p => p.State)
                .ThenInclude(s=>s.Countries)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<List<Place>> GetPlacesByStateIdAsync(Guid stateId)
        {
            return await _appDbContext.Places
                .Include(p => p.State)
                .ThenInclude (s=>s.Countries)
                .Where(p => p.StateId == stateId && !p.IsDeleted)
                .ToListAsync();
        }
        public async Task<List<Place>> GetPlacesByCountryIdAsync(Guid countryId)
        {
            return await _appDbContext.Places
                .Include(p=>p.State)
                .ThenInclude(s=>s.Countries)
                .Where(p => p.State.CountryId == countryId && !p.IsDeleted)
                .ToListAsync();
        }


       
        public async Task UpdatePlaceAsync(Place place)
        {
            _appDbContext.Places.Update(place);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeletePlaceAsync(Guid id)
        {
            var place = await _appDbContext.Places.FindAsync(id);
            if (place != null)
            {
                place.IsDeleted = true;
                await _appDbContext.SaveChangesAsync();
            }
        }

    }
}
