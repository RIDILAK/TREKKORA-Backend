using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class GetWishListDto
    {
        public Guid WishListId {  get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string BestTimeToTravel { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }

    }
}
