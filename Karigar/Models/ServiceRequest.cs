namespace Karigar.Models
{
    public enum RequestStatus
    {
        Requested = 1,
        Confirmed = 2,
        InProgress = 3,
        Completed = 4,
        Cancelled = 5,
        Rejected = 6
    }

    public class ServiceRequest
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int ServiceProviderId { get; set; }
        public int ServiceId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Requested;
        public string? RejectionReason { get; set; }
        public string? CancellationReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public ApplicationUser Customer { get; set; } = null!;
        public ServiceProvider ServiceProvider { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public Review? Review { get; set; }
    }
}

