using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid ?GuideId { get; set; }
        public User Guide { get; set; }

        public Guid ?PlaceId { get; set; }

        public Place Place { get; set; }
        [Range(0.0, 5.0)]
        public decimal RatingValue {  get; set; }
        public string Review { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
