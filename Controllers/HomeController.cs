using Microsoft.AspNetCore.Mvc;
using Karigar.Data;
using Microsoft.EntityFrameworkCore;
using Karigar.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Karigar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? category, string? city, string? search)
        {
            var query = _context.ServiceProviders
                .Include(sp => sp.User)
                .Include(sp => sp.Services)
                .Include(sp => sp.Reviews)
                .Where(sp => sp.IsApproved && sp.IsActive);

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(sp => sp.Category.Contains(category));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(sp => sp.User.City != null && sp.User.City.Contains(city));
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(sp => 
                    sp.BusinessName.Contains(search) || 
                    sp.Description != null && sp.Description.Contains(search) ||
                    sp.Category.Contains(search));
            }

            var providers = await query.ToListAsync();

            var viewModel = providers.Select(sp => new ServiceProviderViewModel
            {
                Id = sp.Id,
                BusinessName = sp.BusinessName,
                Description = sp.Description,
                Category = sp.Category,
                PhoneNumber = sp.PhoneNumber,
                AverageRating = sp.AverageRating,
                TotalReviews = sp.TotalReviews,
                City = sp.User?.City ?? "",
                Services = sp.Services?.Where(s => s.IsActive).Select(s => new ServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Unit = s.Unit
                }).ToList() ?? new List<ServiceViewModel>()
            }).ToList();

            ViewBag.Categories = await _context.ServiceProviders
                .Where(sp => sp.IsApproved && sp.IsActive)
                .Select(sp => sp.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.Cities = await _context.ServiceProviders
                .Include(sp => sp.User)
                .Where(sp => sp.IsApproved && sp.IsActive && sp.User.City != null)
                .Select(sp => sp.User.City!)
                .Distinct()
                .ToListAsync();

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> ProviderDetails(int id)
        {
            var provider = await _context.ServiceProviders
                .Include(sp => sp.User)
                .Include(sp => sp.Services.Where(s => s.IsActive))
                .Include(sp => sp.Reviews)
                    .ThenInclude(r => r.Customer)
                .FirstOrDefaultAsync(sp => sp.Id == id && sp.IsApproved && sp.IsActive);

            if (provider == null)
            {
                return NotFound();
            }

            var viewModel = new ServiceProviderViewModel
            {
                Id = provider.Id,
                BusinessName = provider.BusinessName,
                Description = provider.Description,
                Category = provider.Category,
                PhoneNumber = provider.PhoneNumber,
                AverageRating = provider.AverageRating,
                TotalReviews = provider.TotalReviews,
                City = provider.User.City ?? "",
                Services = provider.Services.Select(s => new ServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Unit = s.Unit
                }).ToList()
            };

            ViewBag.Reviews = provider.Reviews.OrderByDescending(r => r.CreatedAt).Take(10).ToList();
            ViewBag.ProviderId = provider.Id;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

