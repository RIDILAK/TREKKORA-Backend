using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public interface IsearchServices
    {
        Task<Responses<List<SearchResultDto>>> SearchAsync(string query);
    }
    public class SearchServices : IsearchServices

    {
        private readonly ISearchRepository _repository;
        public SearchServices(ISearchRepository repository)
        {
            _repository = repository;
        }
        public async Task<Responses<List<SearchResultDto>>> SearchAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new Responses<List<SearchResultDto>> { Message = "Query is empty", StatuseCode = 400 };
            }

            var result = await _repository.SearchAsync(query);
            return new Responses<List<SearchResultDto>> { Message = "Search completed", Data = result, StatuseCode = 200 };

        }
    }
}
