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
    public class StateRepositories:IStatesRepository
    {
        private readonly AppDbContext _appDbContext;

        public StateRepositories(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<States> AddAsync(States state)
        {
            _appDbContext.States.Add(state);
            await _appDbContext.SaveChangesAsync();
            return state;
        }
       public async Task<IEnumerable<States>> GetAllAsync()
        {
            return await _appDbContext.States.Where(c => !c.IsDeleted).Include(c=>c.Countries).ToListAsync();
        }

      public async  Task<States?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.States.Include(c=>c.Countries).FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

      public async Task<IEnumerable<States>> GetByCountryIdAsync(Guid countryId)
        {
            return await _appDbContext.States.Where(c => c.CountryId == countryId&&!c.IsDeleted).ToListAsync();

        }
       public async Task UpdateAsync(States state)
        {
              _appDbContext.States.Update(state);
            await _appDbContext.SaveChangesAsync();

        }
      public async Task DeleteAsync(States state)
        {
            state.IsDeleted = true;
            _appDbContext.States.Update(state);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Guid> GetByIdPlace(Guid id)
        {
           var state= await _appDbContext.Places
                                  .Where(s => s.Id == id)
                                  .Select(s => s.StateId)
                                  .FirstOrDefaultAsync();
           return state;
        }


    }
}
