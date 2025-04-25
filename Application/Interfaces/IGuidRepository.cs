using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGuidRepository
    {
        Task<GuideProfile> GetByIdAsync(Guid id);
        Task AddAsync(GuideProfile profile);
        Task UpdateAsync(GuideProfile profile);
        Task<List<GuideProfile>> GetAllAsync();
    }
}
