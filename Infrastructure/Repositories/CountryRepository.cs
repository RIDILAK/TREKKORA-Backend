using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CountryRepository:ICountryRepository
    {
        private readonly AppDbContext _context;
        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Countries> GetByNameAsync(string name)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(c => c.CountryName.ToLower() == name.ToLower() && !c.isDeleted);
        }

        public async Task AddAsync(Countries country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Countries>> GetAllAsync()
        {
            return await _context.Countries
                .Where(c => !c.isDeleted)
                .ToListAsync();
        }

        public async Task<Countries?> GetByIdAsync(Guid id)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == id && !c.isDeleted);
        }

        public async Task UpdateAsync(Countries country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Countries country)
        {
            country.isDeleted = true;
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }

       

    }
      
}
