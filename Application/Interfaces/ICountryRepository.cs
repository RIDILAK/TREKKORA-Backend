using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICountryRepository
    {
        Task<Countries> GetByNameAsync(string name);
        Task AddAsync(Countries country);
        Task<List<Countries>> GetAllAsync();
        Task<Countries?> GetByIdAsync(Guid id);
        Task UpdateAsync(Countries country);
        Task DeleteAsync(Countries country);
        
    }
}
