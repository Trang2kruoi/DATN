using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DATN.Data;
using DATN.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "EcomCart";
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // Display Cart
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // Add item to cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1, bool buyNow = false)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = quantity
                });
            }

            SaveCart(cart);

            if (buyNow)
            {
                return RedirectToAction("Checkout", "Orders");
            }

            // If it's an AJAX request, return JSON, else redirect to Cart Page
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, message = "Đã thêm vào giỏ hàng!", cartCount = cart.Sum(i => i.Quantity) });
            }

            return RedirectToAction("Index");
        }

        // Update item quantity
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
                SaveCart(cart);
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var totalAmount = cart.Sum(i => i.TotalPrice);
                var itemTotal = cartItem != null ? cartItem.TotalPrice : 0;
                return Json(new { 
                    success = true, 
                    cartCount = cart.Sum(i => i.Quantity), 
                    itemTotal = itemTotal.ToString("N0") + " đ",
                    totalAmount = totalAmount.ToString("N0") + " đ" 
                });
            }

            return RedirectToAction("Index");
        }

        // Remove item from cart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        // Get total cart item count (for header badge)
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            var count = cart.Sum(i => i.Quantity);
            return Json(new { count });
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

        // Helper: Save cart to session
        private void SaveCart(List<CartItem> cart)
        {
            var sessionData = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, sessionData);
        }
    }
}
