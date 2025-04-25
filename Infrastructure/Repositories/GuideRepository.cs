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
    public class GuideRepository: IGuidRepository
    {
        private readonly AppDbContext _context;
        public GuideRepository(AppDbContext context) => _context = context;

        public async Task<GuideProfile> GetByIdAsync(Guid id)
        {
            return await _context.GuideProfiles.Include(gp => gp.User).FirstOrDefaultAsync(gp => gp.Id == id);
        }

        public async Task AddAsync(GuideProfile profile)
        {
            await _context.GuideProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GuideProfile profile)
        {
            _context.GuideProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GuideProfile>> GetAllAsync()
        {
            return await _context.GuideProfiles.Include(gp => gp.User).Where(gp => !gp.User.IsDeleted).ToListAsync();
        }
    }
}
