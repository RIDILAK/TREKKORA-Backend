using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWishListRepository
    {
        Task<bool> PlaceExistsAsync(Guid productId);
        Task<WishList> GetByUserAndPlaceAsync(Guid userId, Guid placeId);
        Task AddWishLisAsync(WishList wishlist);  
        Task RemoveWishListAsync(WishList wishlist);

        Task<List<WishList>> GetWishListByUserIdAsync(Guid userId);
    }
    
}
