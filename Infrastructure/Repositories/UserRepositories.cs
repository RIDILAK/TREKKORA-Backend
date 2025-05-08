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
    public class UserRepositories:IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(User user)
        {
            user.IsDeleted = true; 
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => !u.IsDeleted && u.Role=="User")
                .ToListAsync();
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u=>u.GuideProfile)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task BlockUser(User user)
        {
            user.IsBlocked=!user.IsBlocked;
             _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
