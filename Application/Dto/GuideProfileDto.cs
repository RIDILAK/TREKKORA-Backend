using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class GuideProfileDto
    {
      
        public string Mobile { get; set; }
     
        public Guid PlaceId { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string AreasCovered { get; set; }

        public string Bio { get; set; }
        public string WhyTravelWithMe { get; set; }
    }
    public class GetGuideProfileDto
    {

        public string Mobile { get; set; }
        public string ProfileImage { get; set; }
        public string PlaceName { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string AreasCovered { get; set; }

        public string Bio { get; set; }
        public string WhyTravelWithMe { get; set; }
    }
    public class GuideDto
    {
       
        public string Name { get; set; }
        public string Email { get; set; }
        public GuideProfileDto GuideProfileDto { get; set; }
      
    }
    public class GetGuideDto 
    {
     public string Name { get; set; }
    public string Email { get; set; }
        public GetGuideProfileDto GetGuideProfileDto { get; set; }
    }
}
