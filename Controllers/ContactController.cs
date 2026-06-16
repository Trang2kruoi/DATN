using Microsoft.AspNetCore.Mvc;
using DATN.Data;
using DATN.Models;

namespace DATN.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        // Action: Contact Page (GET)
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactMessage());
        }

        // Action: Submit Contact Form (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                contactMessage.CreatedDate = DateTime.Now;
                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cảm ơn bạn đã liên hệ! Chúng tôi đã nhận được thông tin và sẽ phản hồi sớm nhất có thể.";
                return RedirectToAction("Index");
            }

            return View(contactMessage);
        }

    }
}
