using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATN.Data;

namespace DATN.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly AppDbContext _context;

        public ArticlesController(AppDbContext context)
        {
            _context = context;
        }

        // Action: News listing
        public async Task<IActionResult> Index()
        {
            var articles = await _context.Articles
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
            return View(articles);
        }

        // Action: News details
        public async Task<IActionResult> Details(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            // Get other recent articles (excluding current one)
            var recentArticles = await _context.Articles
                .Where(a => a.Id != id)
                .OrderByDescending(a => a.CreatedDate)
                .Take(5)
                .ToListAsync();

            ViewBag.RecentArticles = recentArticles;

            return View(article);
        }
    }
}
