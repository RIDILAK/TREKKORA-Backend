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
        [Required]
        public Guid PlaceId { get; set; }
        [Required]
        public Guid GuideId { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Number of people must be between 1 and 10.")]
        public int NumberOfPeople { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
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

        public decimal GuideSalary { get; set; }
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
