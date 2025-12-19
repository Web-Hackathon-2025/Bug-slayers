using Karigar.Models;
using Microsoft.AspNetCore.Identity;

namespace Karigar.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Create roles
            string[] roleNames = { "Admin", "Customer", "ServiceProvider" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create admin user
            if (await userManager.FindByEmailAsync("admin@karigar.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@karigar.com",
                    Email = "admin@karigar.com",
                    FullName = "Admin User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Create sample customer
            if (await userManager.FindByEmailAsync("customer@karigar.com") == null)
            {
                var customerUser = new ApplicationUser
                {
                    UserName = "customer@karigar.com",
                    Email = "customer@karigar.com",
                    FullName = "Test Customer",
                    EmailConfirmed = true,
                    City = "Mumbai",
                    State = "Maharashtra"
                };

                var result = await userManager.CreateAsync(customerUser, "Customer@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, "Customer");
                }
            }

            // Create sample service provider
            if (await userManager.FindByEmailAsync("provider@karigar.com") == null)
            {
                var providerUser = new ApplicationUser
                {
                    UserName = "provider@karigar.com",
                    Email = "provider@karigar.com",
                    FullName = "Test Service Provider",
                    EmailConfirmed = true,
                    City = "Mumbai",
                    State = "Maharashtra"
                };

                var result = await userManager.CreateAsync(providerUser, "Provider@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(providerUser, "ServiceProvider");

                    var serviceProvider = new Models.ServiceProvider
                    {
                        UserId = providerUser.Id,
                        BusinessName = "Expert Plumbing Services",
                        Description = "Professional plumbing services for all your needs",
                        Category = "Plumber",
                        PhoneNumber = "+91-9876543210",
                        IsApproved = true,
                        IsActive = true
                    };

                    context.ServiceProviders.Add(serviceProvider);
                    await context.SaveChangesAsync();

                    // Add sample service
                    var service = new Service
                    {
                        ServiceProviderId = serviceProvider.Id,
                        Name = "Pipe Repair",
                        Description = "Expert pipe repair and replacement",
                        Price = 500,
                        Unit = "per job"
                    };

                    context.Services.Add(service);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

