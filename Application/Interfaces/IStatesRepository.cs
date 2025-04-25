using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IStatesRepository
    {
        Task<States> AddAsync(States state);
        Task<IEnumerable<States>> GetAllAsync();
        Task<States?> GetByIdAsync(Guid id);
        Task<IEnumerable<States>> GetByCountryIdAsync(Guid countryId);
        Task UpdateAsync(States state);
        
        Task DeleteAsync(States state);
    }
}
