using Karigar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Karigar.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.ServiceProvider> ServiceProviders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure ServiceProvider
            builder.Entity<Models.ServiceProvider>()
                .HasOne(sp => sp.User)
                .WithOne(u => u.ServiceProvider)
                .HasForeignKey<Models.ServiceProvider>(sp => sp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Service
            builder.Entity<Service>()
                .HasOne(s => s.ServiceProvider)
                .WithMany(sp => sp.Services)
                .HasForeignKey(s => s.ServiceProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ServiceRequest
            builder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Customer)
                .WithMany(u => u.CustomerRequests)
                .HasForeignKey(sr => sr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceRequest>()
                .HasOne(sr => sr.ServiceProvider)
                .WithMany(sp => sp.ServiceRequests)
                .HasForeignKey(sr => sr.ServiceProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Service)
                .WithMany(s => s.ServiceRequests)
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Review)
                .WithOne(r => r.ServiceRequest)
                .HasForeignKey<Review>(r => r.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Review
            builder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.ServiceProvider)
                .WithMany(sp => sp.Reviews)
                .HasForeignKey(r => r.ServiceProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Availability
            builder.Entity<Availability>()
                .HasOne(a => a.ServiceProvider)
                .WithMany(sp => sp.Availabilities)
                .HasForeignKey(a => a.ServiceProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure one review per service request
            builder.Entity<Review>()
                .HasIndex(r => r.ServiceRequestId)
                .IsUnique();
        }
    }
}

