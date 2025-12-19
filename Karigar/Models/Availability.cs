namespace Karigar.Models
{
    public enum DayOfWeekEnum
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    public class Availability
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        public DayOfWeekEnum DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;
        
        // Navigation properties
        public ServiceProvider ServiceProvider { get; set; } = null!;
    }
}

