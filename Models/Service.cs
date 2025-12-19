using System.ComponentModel.DataAnnotations;

namespace Karigar.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        
        [Required]
        [Display(Name = "Service Name")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Description")]
        public string? Description { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        
        [Display(Name = "Unit")]
        public string? Unit { get; set; } // e.g., "per hour", "per job", "per sq ft"
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public ServiceProvider ServiceProvider { get; set; } = null!;
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}

