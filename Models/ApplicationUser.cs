using Microsoft.AspNetCore.Identity;

namespace Karigar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public ServiceProvider? ServiceProvider { get; set; }
        public ICollection<ServiceRequest> CustomerRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Review> ReviewsGiven { get; set; } = new List<Review>();
    }
}

