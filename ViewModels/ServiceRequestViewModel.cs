using System.ComponentModel.DataAnnotations;
using Karigar.Models;

namespace Karigar.ViewModels
{
    public class ServiceRequestViewModel
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; } = string.Empty;
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public RequestStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();
        public DateTime CreatedAt { get; set; }
        public bool CanReview { get; set; }
        public bool HasReview { get; set; }
    }

    public class CreateServiceRequestViewModel
    {
        public int ServiceProviderId { get; set; }
        public int ServiceId { get; set; }
        
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;
        
        [Display(Name = "Address")]
        public string? Address { get; set; }
        
        [Display(Name = "City")]
        public string? City { get; set; }
        
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        
        [Required]
        [Display(Name = "Requested Date")]
        public DateTime RequestedDate { get; set; } = DateTime.Now;
    }
}

