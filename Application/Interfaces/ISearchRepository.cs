using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<SearchResultDto>> SearchAsync(string query);
    }
}
