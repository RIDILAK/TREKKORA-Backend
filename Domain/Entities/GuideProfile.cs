using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("GuideProfiles", Schema = "guides")]
    public class GuideProfile
    {
        public Guid Id { get; set; }
        public Guid GuideId { get; set; }

        public string ProfileImage { get; set; }
        public string Mobile { get; set; }
 
        public Guid? PlaceId { get; set; }
        public Place Place { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string AreasCovered { get; set; }
        public string Certificates { get; set; }
        public string Bio { get; set; }
        public string WhyTravelWithMe { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 

        public bool ISApproved {  get; set; }=false;

        public User User { get; set; }

        //public ICollection<Rating> Rating { get; set; }

    }
}
