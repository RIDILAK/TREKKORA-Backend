using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class AddBookingDto
    {
       
        public Guid PlaceId { get; set; }
        public Guid GuideId { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetBookingDto
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public string UserName {  get; set; }

        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceImage {  get; set; }
        public Guid GuideId { get; set; }
        public string GuideName { get; set; }
        public string GuideImage {  get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        
    }
  
    public class UpdateBookingDatesDto
    {
      
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class UpdateBookingStatusDto
    {
      
        [Required]
        [RegularExpression("Pending|Approved|Rejected|Completed",ErrorMessage ="Invalid Status,Must be in Approved and Rejected")]
        //[AllowedValues("Rejected", "Approved", "Completed",ErrorMessage ="Invalid Status ,Must be in Approved ,Rejected and Completed")]
        public string Status { get; set; }
    }
}
