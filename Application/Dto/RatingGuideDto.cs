using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class RatingGuideDto
    {
       

        [Required]
        public Guid GuideId { get; set; }

        [Required]
        [Range(0.0, 5.0)]
        public decimal RatingValue { get; set; }

        public string Review { get; set; }
    }

    public class CreateRatingForPlaceDto
    {
        
      

        [Required]
        public Guid PlaceId { get; set; }

        [Required]
        [Range(0.0, 5.0)]
        public decimal RatingValue { get; set; }

        public string Review { get; set; }
    }

    public class GetRatingGuideDto  
    {
        public Guid Id { get; set; }

        public string UserName {  get; set; }

        public Guid GuideId{get;set;}
        public string GuideName {  get; set; }  
        public decimal RatingValue { get; set; }
        public string Review { get; set; }
    }

    public class GetRatingPlaceDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public decimal RatingValue { get; set; }
        public string Review { get; set; }
    }

    public class GetRatingByUser
    {
        public Guid Id { get; set; }

        public Guid GuideId { get; set; }
        public string GuideName { get; set; }
        public decimal RatingValue { get; set; }
        public string Review { get; set; }

        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
       

    }
}
