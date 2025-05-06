using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SearchReposiotry:ISearchRepository
    {
        private readonly AppDbContext _context;

        public SearchReposiotry(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SearchResultDto>> SearchAsync(string query)
        {
            query = query.ToLower();

            var guides = await _context.Users
                .Include(p=>p.GuideProfile)
    .Where(u => u.Role == "Guide" && u.Name.ToLower().Contains(query))
    .Select(u => new SearchResultDto
    {
        Type = "Guide",
        Name = u.Name,
        ImageUrl = u.GuideProfile.ProfileImage,
        Id = u.Id
    })
    .ToListAsync();

            var places = await _context.Places
           .Where(p => p.PlaceName.ToLower().Contains(query))
           .Select(p => new SearchResultDto
           {
               Type = "Place",
               Name = p.PlaceName,
               ImageUrl=p.ImageUrl,
               Id = p.Id
           }).ToListAsync();

            return guides.Concat(places).ToList(); 
        }
    }
}
