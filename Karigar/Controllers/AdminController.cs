using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Karigar.Data;
using Karigar.Models;
using Microsoft.EntityFrameworkCore;

namespace Karigar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalUsers = await _context.Users.CountAsync();
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            ViewBag.TotalCustomers = customers.Count;
            ViewBag.TotalServiceProviders = await _context.ServiceProviders.CountAsync();
            ViewBag.PendingApprovals = await _context.ServiceProviders
                .Where(sp => !sp.IsApproved).CountAsync();
            ViewBag.TotalRequests = await _context.ServiceRequests.CountAsync();
            ViewBag.ActiveRequests = await _context.ServiceRequests
                .Where(sr => sr.Status == RequestStatus.Requested || 
                    sr.Status == RequestStatus.Confirmed || 
                    sr.Status == RequestStatus.InProgress)
                .CountAsync();
            ViewBag.TotalReviews = await _context.Reviews.CountAsync();

            return View();
        }

        public async Task<IActionResult> ManageUsers(string? role, string? search)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(role))
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role);
                var userIds = usersInRole.Select(u => u.Id);
                query = query.Where(u => userIds.Contains(u.Id));
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => 
                    u.FullName != null && u.FullName.Contains(search) ||
                    u.Email.Contains(search));
            }

            var users = await query.ToListAsync();
            var userViewModels = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new
                {
                    User = user,
                    Roles = roles,
                    IsServiceProvider = await _context.ServiceProviders
                        .AnyAsync(sp => sp.UserId == user.Id)
                });
            }

            ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return View(userViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuspendUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User suspended successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to suspend user.";
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnabled = false;
            user.LockoutEnd = null;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User activated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to activate user.";
            }

            return RedirectToAction("ManageUsers");
        }

        public async Task<IActionResult> ManageServiceProviders(string? status, string? search)
        {
            var query = _context.ServiceProviders
                .Include(sp => sp.User)
                .Include(sp => sp.Services)
                .Include(sp => sp.Reviews)
                .AsQueryable();

            if (status == "pending")
            {
                query = query.Where(sp => !sp.IsApproved);
            }
            else if (status == "approved")
            {
                query = query.Where(sp => sp.IsApproved && sp.IsActive);
            }
            else if (status == "suspended")
            {
                query = query.Where(sp => !sp.IsActive);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(sp => 
                    sp.BusinessName.Contains(search) ||
                    sp.Category.Contains(search) ||
                    (sp.User.City != null && sp.User.City.Contains(search)));
            }

            var providers = await query.OrderByDescending(sp => sp.CreatedAt).ToListAsync();
            return View(providers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveServiceProvider(int id)
        {
            var provider = await _context.ServiceProviders.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            provider.IsApproved = true;
            provider.IsActive = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Service provider approved successfully.";
            return RedirectToAction("ManageServiceProviders");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuspendServiceProvider(int id)
        {
            var provider = await _context.ServiceProviders.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            provider.IsActive = false;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Service provider suspended successfully.";
            return RedirectToAction("ManageServiceProviders");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveServiceProvider(int id)
        {
            var provider = await _context.ServiceProviders
                .Include(sp => sp.Services)
                .Include(sp => sp.ServiceRequests)
                .FirstOrDefaultAsync(sp => sp.Id == id);

            if (provider == null)
            {
                return NotFound();
            }

            // Check if there are active requests
            var activeRequests = await _context.ServiceRequests
                .Where(sr => sr.ServiceProviderId == id && 
                    (sr.Status == RequestStatus.Requested || 
                     sr.Status == RequestStatus.Confirmed || 
                     sr.Status == RequestStatus.InProgress))
                .CountAsync();

            if (activeRequests > 0)
            {
                TempData["ErrorMessage"] = "Cannot remove service provider with active requests. Please suspend instead.";
                return RedirectToAction("ManageServiceProviders");
            }

            _context.ServiceProviders.Remove(provider);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Service provider removed successfully.";
            return RedirectToAction("ManageServiceProviders");
        }

        public async Task<IActionResult> MonitorListings(string? category, string? search)
        {
            var query = _context.ServiceProviders
                .Include(sp => sp.User)
                .Include(sp => sp.Services)
                .Include(sp => sp.Reviews)
                .Where(sp => sp.IsApproved)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(sp => sp.Category.Contains(category));
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(sp => 
                    sp.BusinessName.Contains(search) ||
                    sp.Description != null && sp.Description.Contains(search));
            }

            var providers = await query.OrderByDescending(sp => sp.CreatedAt).ToListAsync();
            ViewBag.Categories = await _context.ServiceProviders
                .Where(sp => sp.IsApproved)
                .Select(sp => sp.Category)
                .Distinct()
                .ToListAsync();

            return View(providers);
        }

        public async Task<IActionResult> ModerateReviews(string? search)
        {
            var query = _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.ServiceProvider)
                .Include(r => r.ServiceRequest)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r => 
                    r.ServiceProvider.BusinessName.Contains(search) ||
                    (r.Customer.FullName != null && r.Customer.FullName.Contains(search)) ||
                    (r.Comment != null && r.Comment.Contains(search)));
            }

            var reviews = await query.OrderByDescending(r => r.CreatedAt).ToListAsync();
            return View(reviews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Review deleted successfully.";
            return RedirectToAction("ModerateReviews");
        }

        public async Task<IActionResult> ViewMetrics()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            var hasReviews = await _context.Reviews.AnyAsync();
            var averageRating = hasReviews ? await _context.Reviews.AverageAsync(r => (double)r.Rating) : 0.0;
            
            var metrics = new
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalCustomers = customers.Count,
                TotalServiceProviders = await _context.ServiceProviders.CountAsync(),
                ApprovedProviders = await _context.ServiceProviders.Where(sp => sp.IsApproved).CountAsync(),
                PendingProviders = await _context.ServiceProviders.Where(sp => !sp.IsApproved).CountAsync(),
                TotalServices = await _context.Services.CountAsync(),
                ActiveServices = await _context.Services.Where(s => s.IsActive).CountAsync(),
                TotalRequests = await _context.ServiceRequests.CountAsync(),
                RequestedRequests = await _context.ServiceRequests.Where(sr => sr.Status == RequestStatus.Requested).CountAsync(),
                ConfirmedRequests = await _context.ServiceRequests.Where(sr => sr.Status == RequestStatus.Confirmed).CountAsync(),
                CompletedRequests = await _context.ServiceRequests.Where(sr => sr.Status == RequestStatus.Completed).CountAsync(),
                CancelledRequests = await _context.ServiceRequests.Where(sr => sr.Status == RequestStatus.Cancelled).CountAsync(),
                TotalReviews = await _context.Reviews.CountAsync(),
                AverageRating = averageRating,
                RequestsByCategory = await _context.ServiceRequests
                    .Include(sr => sr.ServiceProvider)
                    .GroupBy(sr => sr.ServiceProvider.Category)
                    .Select(g => new { Category = g.Key ?? "Unknown", Count = g.Count() })
                    .ToListAsync()
            };

            return View(metrics);
        }
    }
}

