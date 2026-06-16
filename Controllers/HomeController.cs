using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATN.Data;
using DATN.Models;
using System.Diagnostics;

namespace DATN.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch categories for navigation/display
            var categories = await _context.Categories.Take(5).ToListAsync();

            // Fetch products for different sections
            var featuredProducts = await _context.Products
                .Include(p => p.Brand)
                .Where(p => p.IsFeatured)
                .Take(8)
                .ToListAsync();

            var saleProducts = await _context.Products
                .Include(p => p.Brand)
                .Where(p => p.IsSale)
                .Take(8)
                .ToListAsync();

            var bestSellers = await _context.Products
                .Include(p => p.Brand)
                .Where(p => p.IsBestSeller)
                .Take(8)
                .ToListAsync();

            ViewBag.Categories = categories;
            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.SaleProducts = saleProducts;
            ViewBag.BestSellers = bestSellers;

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
