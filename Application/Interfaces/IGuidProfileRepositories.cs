using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGuidProfileRepositories
    {
        Task<List<User>> GetAllGuidesAsync();
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<bool> ExistsAsync(Guid id);
        Task<List<User>> GetUnapprovedGuides();
    }
}
