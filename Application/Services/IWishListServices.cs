using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public interface IWishListServices
    {

        Task<Responses<string>> AddOrRemoveWishList(Guid placeId, Guid userId);

        Task<Responses<List<GetWishListDto>>> GetWishList(Guid userId);

    }
    public class WishListService:IWishListServices
    {
        private readonly IWishListRepository _repository;
        private readonly IMapper _mapper;

        public WishListService(IWishListRepository repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Responses<string>> AddOrRemoveWishList(Guid placeId, Guid userId)
        {
            var placeExist = await _repository.PlaceExistsAsync(placeId);
            if (placeExist == null)
            {
                return new Responses<string> { Message = "Place is not exist", StatuseCode = 400 };

            }

            var wishListItem = await _repository.GetByUserAndPlaceAsync(userId, placeId);
            if (wishListItem != null)
            {

                await _repository.RemoveWishListAsync(wishListItem);
                return new Responses<string> { Message = "Item Removed From WishList", StatuseCode = 200 };
            }
            else
            {
                var newWishListItems = new WishList
                {
                    PlaceId = placeId,
                    UserId = userId,
                };
                await _repository.AddWishLisAsync(newWishListItems);
                return new Responses<string> { Message = "Place Added To the WishList", StatuseCode = 200 };
            }


        }
        public async Task<Responses<List<GetWishListDto>>> GetWishList(Guid userId)
        {
            try
            {
               

               

                var wishListItems = await _repository.GetWishListByUserIdAsync(userId);

                if (wishListItems.Count > 0)
                {
                    var wishListDtos = wishListItems.Select(x => new GetWishListDto
                    {
                        WishListId = x.Id, 
                        PlaceId = x.PlaceId,
                        PlaceName = x.Place.PlaceName,
                        Price = x.Place.Price.ToString(),
                      BestTimeToTravel=x.Place.BestTimeToTravel,
                      ImageUrl=x.Place.ImageUrl,
                        Description = x.Place.Description,
                       
                    }).ToList();

                    return new Responses<List<GetWishListDto>>
                    {
                        StatuseCode = 200,
                        Message = "Get wishlist success",
                        Data = wishListDtos
                    };
                }

                return new Responses<List<GetWishListDto>>
                {
                    StatuseCode = 200,
                    Message = "Wishlist is empty"
                };
            }
            catch (Exception ex)
            {
                return new Responses<List<GetWishListDto>>
                {
                    StatuseCode = 500,
                    Message = ex.Message
                };
            }
        }

    }
}
