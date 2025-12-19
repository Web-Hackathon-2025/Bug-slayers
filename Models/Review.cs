namespace Karigar.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int ServiceProviderId { get; set; }
        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public ServiceRequest ServiceRequest { get; set; } = null!;
        public ApplicationUser Customer { get; set; } = null!;
        public ServiceProvider ServiceProvider { get; set; } = null!;
    }
}

