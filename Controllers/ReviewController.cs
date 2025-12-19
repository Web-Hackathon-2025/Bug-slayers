using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Karigar.Data;
using Karigar.Models;
using Microsoft.EntityFrameworkCore;

namespace Karigar.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int requestId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var request = await _context.ServiceRequests
                .Include(sr => sr.ServiceProvider)
                .Include(sr => sr.Service)
                .Include(sr => sr.Review)
                .FirstOrDefaultAsync(sr => sr.Id == requestId && sr.CustomerId == user.Id);

            if (request == null)
            {
                return NotFound();
            }

            if (request.Status != RequestStatus.Completed)
            {
                TempData["ErrorMessage"] = "You can only review completed services.";
                return RedirectToAction("MyRequests", "Customer");
            }

            if (request.Review != null)
            {
                TempData["ErrorMessage"] = "You have already reviewed this service.";
                return RedirectToAction("MyRequests", "Customer");
            }

            ViewBag.Request = request;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int requestId, int rating, string? comment)
        {
            if (rating < 1 || rating > 5)
            {
                ModelState.AddModelError("Rating", "Rating must be between 1 and 5.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var request = await _context.ServiceRequests
                .Include(sr => sr.Review)
                .FirstOrDefaultAsync(sr => sr.Id == requestId && sr.CustomerId == user.Id);

            if (request == null)
            {
                return NotFound();
            }

            if (request.Status != RequestStatus.Completed || request.Review != null)
            {
                TempData["ErrorMessage"] = "Cannot create review for this request.";
                return RedirectToAction("MyRequests", "Customer");
            }

            if (ModelState.IsValid)
            {
                var review = new Review
                {
                    ServiceRequestId = requestId,
                    CustomerId = user.Id,
                    ServiceProviderId = request.ServiceProviderId,
                    Rating = rating,
                    Comment = comment,
                    CreatedAt = DateTime.Now
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Review submitted successfully!";
                return RedirectToAction("MyRequests", "Customer");
            }

            request = await _context.ServiceRequests
                .Include(sr => sr.ServiceProvider)
                .Include(sr => sr.Service)
                .FirstOrDefaultAsync(sr => sr.Id == requestId);

            ViewBag.Request = request;
            return View();
        }
    }
}

