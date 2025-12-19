using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Karigar.Models
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; } = string.Empty;
        
        [Display(Name = "Description")]
        public string? Description { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; } = string.Empty;
        
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
        
        // Calculated properties
        public double AverageRating => Reviews != null && Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        public int TotalReviews => Reviews?.Count ?? 0;
    }
}

