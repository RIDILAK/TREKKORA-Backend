using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Users", Schema = "auth")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(20, ErrorMessage = "Role can't be longer than 20 characters")]
        //[AllowedValues("User,G")]
        public string Role { get; set; }

        public bool IsBlocked { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public GuideProfile GuideProfile { get; set; }

        public List<WishList>wishLists { get; set; }

        public ICollection<Rating> Ratingss { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }


   
}



