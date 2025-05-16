using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class GuideProfileDto
    {

        [Required(ErrorMessage = "Mobile number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, ErrorMessage = "Mobile number can't exceed 15 digits")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Place ID is required")]
        public Guid PlaceId { get; set; }

        [Required(ErrorMessage = "Experience is required")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]
        public int Experience { get; set; }

        [Required(ErrorMessage = "Languages are required")]
        [StringLength(100, ErrorMessage = "Languages should not exceed 100 characters")]
        public string Languages { get; set; }

        [Required(ErrorMessage = "Areas Covered is required")]
        [StringLength(200, ErrorMessage = "Areas Covered should not exceed 200 characters")]
        public string AreasCovered { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        [StringLength(1000, ErrorMessage = "Bio should not exceed 1000 characters")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "Why Travel With Me is required")]
        [StringLength(1000, ErrorMessage = "Why Travel With Me should not exceed 1000 characters")]
        public string WhyTravelWithMe { get; set; }

    }
    public class GetGuideProfileDto
    {
        public string GuideId { get; set; }
        public string Mobile { get; set; }
        public string ProfileImage { get; set; }
        public string PlaceName { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string AreasCovered { get; set; }

        public bool isAvailable { get; set; }
        public string Bio { get; set; }
        public string WhyTravelWithMe { get; set; }
        public string Certificates { get; set; }
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

        public static implicit operator List<object>(GetGuideDto v)
        {
            throw new NotImplementedException();
        }
    }
}
