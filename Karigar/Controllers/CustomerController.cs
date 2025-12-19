using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Karigar.Data;
using Karigar.Models;
using Karigar.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Karigar.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateRequest(int serviceProviderId, int serviceId)
        {
            var provider = await _context.ServiceProviders
                .Include(sp => sp.Services)
                .FirstOrDefaultAsync(sp => sp.Id == serviceProviderId && sp.IsApproved && sp.IsActive);

            if (provider == null)
            {
                return NotFound();
            }

            var service = provider.Services.FirstOrDefault(s => s.Id == serviceId && s.IsActive);
            if (service == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var viewModel = new CreateServiceRequestViewModel
            {
                ServiceProviderId = serviceProviderId,
                ServiceId = serviceId,
                City = user.City ?? "",
                Address = user.Address ?? "",
                PhoneNumber = user.PhoneNumber ?? ""
            };

            ViewBag.ServiceProviderName = provider.BusinessName;
            ViewBag.ServiceName = service.Name;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CreateServiceRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                var serviceRequest = new ServiceRequest
                {
                    CustomerId = user.Id,
                    ServiceProviderId = model.ServiceProviderId,
                    ServiceId = model.ServiceId,
                    Description = model.Description,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.PhoneNumber,
                    RequestedDate = model.RequestedDate,
                    Status = RequestStatus.Requested,
                    CreatedAt = DateTime.Now
                };

                _context.ServiceRequests.Add(serviceRequest);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Service request submitted successfully!";
                return RedirectToAction("MyRequests");
            }

            return View(model);
        }

        public async Task<IActionResult> MyRequests()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var requests = await _context.ServiceRequests
                .Include(sr => sr.ServiceProvider)
                .Include(sr => sr.Service)
                .Include(sr => sr.Review)
                .Where(sr => sr.CustomerId == user.Id)
                .OrderByDescending(sr => sr.CreatedAt)
                .ToListAsync();

            var viewModel = requests.Select(sr => new ServiceRequestViewModel
            {
                Id = sr.Id,
                ServiceProviderId = sr.ServiceProviderId,
                ServiceProviderName = sr.ServiceProvider.BusinessName,
                ServiceId = sr.ServiceId,
                ServiceName = sr.Service.Name,
                Description = sr.Description,
                Address = sr.Address,
                City = sr.City,
                PhoneNumber = sr.PhoneNumber,
                RequestedDate = sr.RequestedDate,
                ScheduledDate = sr.ScheduledDate,
                Status = sr.Status,
                CreatedAt = sr.CreatedAt,
                CanReview = sr.Status == RequestStatus.Completed && sr.Review == null,
                HasReview = sr.Review != null
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelRequest(int id, string? reason)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.Id == id && sr.CustomerId == user.Id);

            if (request == null)
            {
                return NotFound();
            }

            if (request.Status == RequestStatus.Completed)
            {
                TempData["ErrorMessage"] = "Cannot cancel a completed request.";
                return RedirectToAction("MyRequests");
            }

            request.Status = RequestStatus.Cancelled;
            request.CancellationReason = reason;
            request.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Request cancelled successfully.";
            return RedirectToAction("MyRequests");
        }
    }
}

