namespace Karigar.ViewModels
{
    public class ServiceProviderViewModel
    {
        public int Id { get; set; }
        public string BusinessName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public string City { get; set; } = string.Empty;
        public List<ServiceViewModel> Services { get; set; } = new();
    }

    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Unit { get; set; }
    }
}

