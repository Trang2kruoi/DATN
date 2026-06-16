using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATN.Data;
using DATN.Models;

namespace DATN.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly AppDbContext _context;

        public RecruitmentController(AppDbContext context)
        {
            _context = context;
        }

        // Action: Job listings
        public async Task<IActionResult> Index()
        {
            var jobs = await _context.RecruitmentPosts
                .Where(r => r.Deadline >= DateTime.Today)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
            return View(jobs);
        }

        // Action: Job detail view
        public async Task<IActionResult> Details(int id)
        {
            var job = await _context.RecruitmentPosts.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // Action: Job application form (GET)
        [HttpGet]
        public async Task<IActionResult> Apply(int id)
        {
            var job = await _context.RecruitmentPosts.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            ViewBag.Job = job;
            return View(new JobApplication { RecruitmentPostId = id });
        }

        // Action: Submit Application (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(JobApplication application)
        {
            var job = await _context.RecruitmentPosts.FindAsync(application.RecruitmentPostId);
            if (job == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                application.AppliedDate = DateTime.Now;
                _context.JobApplications.Add(application);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đơn ứng tuyển vào vị trí \"{job.Title}\" của bạn đã được gửi thành công! Bộ phận Nhân sự sẽ liên hệ với bạn trong thời gian sớm nhất.";
                return RedirectToAction("Details", new { id = job.Id });
            }

            ViewBag.Job = job;
            return View(application);
        }
    }
}
