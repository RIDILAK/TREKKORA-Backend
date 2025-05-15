using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Bookings", Schema = "transactions")]
    public class Booking
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PlaceId { get; set; }
        public Place place { get; set; }

        public Guid GuideId { get; set; }
        public User Guide { get; set; }

        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool isDeleted {  get; set; }=false;

        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; }

    }
}
