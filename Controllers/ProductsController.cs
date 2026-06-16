using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATN.Data;
using DATN.Models;

namespace DATN.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // Action: Product catalog with search, filter, and sorting
        public async Task<IActionResult> Index(string? search, int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy)
        {
            var query = _context.Products.Include(p => p.Category).Include(p => p.Brand).AsQueryable();

            // 1. Search Query
            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch) || p.SKU.ToLower().Contains(lowerSearch) || (p.Description != null && p.Description.ToLower().Contains(lowerSearch)));
            }

            // 2. Category Filter
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // 3. Price Filter
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // 4. Sorting
            switch (sortBy)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case "name_asc":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "newest":
                default:
                    query = query.OrderByDescending(p => p.Id);
                    break;
            }

            var products = await query.ToListAsync();
            var categories = await _context.Categories.ToListAsync();

            // Retain values in ViewBag for the view controls
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = categoryId;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SearchQuery = search;
            ViewBag.SortBy = sortBy;

            return View(products);
        }

        // Action: Product Details and Related Products
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Fetch related products (same category, excluding current product)
            var relatedProducts = await _context.Products
                .Include(p => p.Brand)
                .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                .Take(4)
                .ToListAsync();

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }

        // Action: Best Sellers landing page
        public async Task<IActionResult> BestSellers()
        {
            var bestSellers = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.IsBestSeller)
                .ToListAsync();

            return View(bestSellers);
        }
    }
}
