using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DATN.Data;
using DATN.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN.Controllers
{
    public class OrdersController : Controller
    {
        private const string CartSessionKey = "EcomCart";
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // Action: Checkout Form
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (cart.Count == 0)
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm trước khi thanh toán.";
                return RedirectToAction("Index", "Cart");
            }

            ViewBag.Cart = cart;
            ViewBag.TotalAmount = cart.Sum(i => i.TotalPrice);

            return View(new Order());
        }

        // Action: Place Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart = GetCart();
            if (cart.Count == 0)
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống!");
                ViewBag.Cart = cart;
                ViewBag.TotalAmount = 0;
                return View(order);
            }

            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.TotalAmount = cart.Sum(i => i.TotalPrice);

                // Add OrderDetails and update product stock
                foreach (var item in cart)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        // Check stock
                        if (product.Stock < item.Quantity)
                        {
                            ModelState.AddModelError("", $"Sản phẩm {product.Name} chỉ còn {product.Stock} sản phẩm trong kho.");
                            ViewBag.Cart = cart;
                            ViewBag.TotalAmount = cart.Sum(i => i.TotalPrice);
                            return View(order);
                        }

                        // Deduct stock
                        product.Stock -= item.Quantity;

                        var detail = new OrderDetail
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = item.Price
                        };
                        order.OrderDetails.Add(detail);
                    }
                }

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Clear cart from Session
                HttpContext.Session.Remove(CartSessionKey);

                // Redirect to Success action with order Id
                return RedirectToAction("Success", new { id = order.Id });
            }

            ViewBag.Cart = cart;
            ViewBag.TotalAmount = cart.Sum(i => i.TotalPrice);
            return View(order);
        }

        // Action: Order Success
        public async Task<IActionResult> Success(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // Helper: Get cart from session
        private List<CartItem> GetCart()
        {
            var sessionData = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new List<CartItem>();
            }
            try
            {
                return JsonSerializer.Deserialize<List<CartItem>>(sessionData) ?? new List<CartItem>();
            }
            catch
            {
                return new List<CartItem>();
            }
        }
    }
}
