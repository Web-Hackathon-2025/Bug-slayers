using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Karigar.Data;
using Karigar.Models;
using Microsoft.EntityFrameworkCore;
using ServiceProviderModel = Karigar.Models.ServiceProvider;

namespace Karigar.Controllers
{
    [Authorize(Roles = "ServiceProvider")]
    public class ServiceProviderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceProviderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .Include(sp => sp.Services)
                .Include(sp => sp.ServiceRequests)
                .Include(sp => sp.Reviews)
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return RedirectToAction("CreateProfile");
            }

            ViewBag.PendingRequests = await _context.ServiceRequests
                .Include(sr => sr.Customer)
                .Include(sr => sr.Service)
                .Where(sr => sr.ServiceProviderId == provider.Id && sr.Status == RequestStatus.Requested)
                .OrderByDescending(sr => sr.CreatedAt)
                .ToListAsync();

            ViewBag.ConfirmedRequests = await _context.ServiceRequests
                .Include(sr => sr.Customer)
                .Include(sr => sr.Service)
                .Where(sr => sr.ServiceProviderId == provider.Id && 
                    (sr.Status == RequestStatus.Confirmed || sr.Status == RequestStatus.InProgress))
                .OrderBy(sr => sr.ScheduledDate)
                .ToListAsync();

            return View(provider);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var existingProvider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (existingProvider != null)
            {
                return RedirectToAction("EditProfile");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfile(ServiceProviderModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var provider = new ServiceProviderModel
                {
                    UserId = user.Id,
                    BusinessName = model.BusinessName,
                    Description = model.Description,
                    Category = model.Category,
                    PhoneNumber = model.PhoneNumber ?? user.PhoneNumber,
                    IsApproved = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.ServiceProviders.Add(provider);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile created successfully! Waiting for admin approval.";
                return RedirectToAction("Dashboard");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return RedirectToAction("CreateProfile");
            }

            return View(provider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ServiceProviderModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                provider.BusinessName = model.BusinessName;
                provider.Description = model.Description;
                provider.Category = model.Category;
                provider.PhoneNumber = model.PhoneNumber;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Dashboard");
            }

            return View(provider);
        }

        [HttpGet]
        public async Task<IActionResult> ManageServices()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .Include(sp => sp.Services)
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return RedirectToAction("CreateProfile");
            }

            return View(provider.Services.ToList());
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(Service model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return RedirectToAction("CreateProfile");
            }

            if (ModelState.IsValid)
            {
                var service = new Service
                {
                    ServiceProviderId = provider.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Unit = model.Unit,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Services.Add(service);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Service added successfully!";
                return RedirectToAction("ManageServices");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id && s.ServiceProviderId == provider.Id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(Service model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == model.Id && s.ServiceProviderId == provider.Id);

            if (service == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                service.Name = model.Name;
                service.Description = model.Description;
                service.Price = model.Price;
                service.Unit = model.Unit;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Service updated successfully!";
                return RedirectToAction("ManageServices");
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteService(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id && s.ServiceProviderId == provider.Id);

            if (service == null)
            {
                return NotFound();
            }

            service.IsActive = false;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Service deactivated successfully!";
            return RedirectToAction("ManageServices");
        }

        [HttpGet]
        public async Task<IActionResult> ManageRequests()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return RedirectToAction("CreateProfile");
            }

            var requests = await _context.ServiceRequests
                .Include(sr => sr.Customer)
                .Include(sr => sr.Service)
                .Where(sr => sr.ServiceProviderId == provider.Id)
                .OrderByDescending(sr => sr.CreatedAt)
                .ToListAsync();

            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptRequest(int id, DateTime? scheduledDate)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return NotFound();
            }

            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.Id == id && sr.ServiceProviderId == provider.Id);

            if (request == null)
            {
                return NotFound();
            }

            if (request.Status != RequestStatus.Requested)
            {
                TempData["ErrorMessage"] = "This request cannot be accepted.";
                return RedirectToAction("ManageRequests");
            }

            request.Status = RequestStatus.Confirmed;
            request.ScheduledDate = scheduledDate ?? request.RequestedDate;
            request.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Request accepted successfully!";
            return RedirectToAction("ManageRequests");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectRequest(int id, string? reason)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return NotFound();
            }

            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.Id == id && sr.ServiceProviderId == provider.Id);

            if (request == null)
            {
                return NotFound();
            }

            if (request.Status != RequestStatus.Requested)
            {
                TempData["ErrorMessage"] = "This request cannot be rejected.";
                return RedirectToAction("ManageRequests");
            }

            request.Status = RequestStatus.Rejected;
            request.RejectionReason = reason;
            request.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Request rejected.";
            return RedirectToAction("ManageRequests");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRequestStatus(int id, RequestStatus status)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return NotFound();
            }

            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.Id == id && sr.ServiceProviderId == provider.Id);

            if (request == null)
            {
                return NotFound();
            }

            request.Status = status;
            request.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Request status updated successfully!";
            return RedirectToAction("ManageRequests");
        }

        [HttpGet]
        public async Task<IActionResult> BookingHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var provider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == user.Id);

            if (provider == null)
            {
                return RedirectToAction("CreateProfile");
            }

            var requests = await _context.ServiceRequests
                .Include(sr => sr.Customer)
                .Include(sr => sr.Service)
                .Include(sr => sr.Review)
                .Where(sr => sr.ServiceProviderId == provider.Id && 
                    (sr.Status == RequestStatus.Confirmed || sr.Status == RequestStatus.Completed))
                .OrderByDescending(sr => sr.ScheduledDate)
                .ToListAsync();

            return View(requests);
        }
    }
}

