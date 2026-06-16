// ==========================================================================
// Smyou Pro Shopping Cart Helpers (cart.js)
// ==========================================================================

document.addEventListener("DOMContentLoaded", function () {
    // Intercept form submissions for Add to Cart with class .ajax-cart-form
    document.body.addEventListener("submit", function (event) {
        if (event.target && event.target.classList.contains("ajax-cart-form")) {
            event.preventDefault();
            
            var form = event.target;
            var formData = new FormData(form);
            
            // Build URL-encoded string
            var urlEncodedData = [];
            for (var pair of formData.entries()) {
                urlEncodedData.push(encodeURIComponent(pair[0]) + "=" + encodeURIComponent(pair[1]));
            }
            var bodyString = urlEncodedData.join("&");
            
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/Cart/AddToCart", true);
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        try {
                            var response = JSON.parse(xhr.responseText);
                            if (response.success) {
                                // Update header badge
                                var badge = document.getElementById("cart-badge");
                                if (badge) {
                                    badge.innerText = response.cartCount;
                                    
                                    // Add temporary animation class
                                    badge.classList.add("pulse-animation");
                                    setTimeout(function() {
                                        badge.classList.remove("pulse-animation");
                                    }, 500);
                                }
                                
                                // Show success message
                                showToast(response.message || "Đã thêm vào giỏ hàng!");
                            } else {
                                showToast("Có lỗi xảy ra khi thêm vào giỏ hàng.", false);
                            }
                        } catch (e) {
                            console.error("Lỗi xử lý phản hồi từ server:", e);
                            form.submit(); // Fallback if parsing fails
                        }
                    } else {
                        console.error("Lỗi kết nối server:", xhr.statusText);
                        form.submit(); // Fallback to normal post
                    }
                }
            };
            
            xhr.send(bodyString);
        }
    });
});
