using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GuidProfileRepository : IGuidProfileRepositories
    {
        private readonly AppDbContext _context;
        public GuidProfileRepository(AppDbContext context) => _context = context;

        public async Task<List<User>> GetAllGuidesAsync()
        {
            return await _context.Users
                .Include(u => u.GuideProfile)
                    .ThenInclude(p => p.Place)
                .Where(u => u.Role == "Guide"
                            && !u.IsDeleted
                            && u.GuideProfile != null
                            && u.GuideProfile.ISApproved == true
                            && u.GuideProfile.isAvailable==true)
                .ToListAsync();
        }


        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.GuideProfile)
                .FirstOrDefaultAsync(u => u.Id == id && u.Role == "Guide" && !u.IsDeleted);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            user.IsDeleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id && !u.IsDeleted);
        }

       public async Task<List<User>> GetUnapprovedGuides()
        {
            return await _context.Users
                .Include(u => u.GuideProfile)
                .Where(u=>u.Role=="Guide" &&
                !u.IsDeleted &&
                u.GuideProfile != null &&
                u.GuideProfile.ISApproved==false)
                .ToListAsync();

        }
    }
}