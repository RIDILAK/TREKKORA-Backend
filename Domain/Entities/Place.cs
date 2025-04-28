using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Place
    {
        public Guid Id { get; set; }
        public string PlaceName {  get; set; }
        public Guid StateId {  get; set; }
        public string Pincode { get; set; }
        public string BestTimeToTravel { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  
        public States State { get; set; }

        public List<WishList> WishList { get; set; }

        public ICollection<Rating> Rating { get; set; }

    }
}
