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
    public class WishListRepository : IWishListRepository
    {
        private readonly AppDbContext _appDbContext;

        public WishListRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<WishList> GetByUserAndPlaceAsync(Guid userId, Guid placeId)
        {
            return await _appDbContext.WishList.FirstOrDefaultAsync(c => c.UserId == userId && c.PlaceId == placeId);

        }

        public async Task AddWishLisAsync(WishList wishlist)
        {
            await _appDbContext.WishList.AddAsync(wishlist);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveWishListAsync(WishList wishlist)
        {
             _appDbContext.WishList.Remove(wishlist);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<WishList>> GetWishListByUserIdAsync(Guid userId)
        {
            return await _appDbContext.WishList.Include(c => c.Place)
                .ThenInclude(c=>c.Images)
                .Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<bool> PlaceExistsAsync(Guid placeId)
        {
            return await _appDbContext.Places.AnyAsync(x => x.Id == placeId);
        }

    }
}

