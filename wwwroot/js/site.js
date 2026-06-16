// ==========================================================================
// Smyou Pro Global Javascript Helpers (site.js)
// ==========================================================================

document.addEventListener("DOMContentLoaded", function () {
    // 1. Initial Cart Count Load
    updateHeaderCartCount();

    // 2. Alert dismissal automatic timeout (optional, e.g., 5 seconds)
    const autoDismissAlerts = document.querySelectorAll(".alert-dismissible");
    autoDismissAlerts.forEach(function (alert) {
        setTimeout(function () {
            alert.style.transition = "opacity 0.5s ease-out";
            alert.style.opacity = "0";
            setTimeout(function () {
                alert.style.display = "none";
            }, 500);
        }, 8000);
    });
});

// Helper: Show custom Toast message
function showToast(message, isSuccess = true) {
    const container = document.getElementById("toast-container");
    if (!container) return;

    const toast = document.createElement("div");
    toast.className = "toast";
    
    const icon = document.createElement("i");
    icon.className = isSuccess ? "fas fa-check-circle" : "fas fa-exclamation-circle";
    icon.style.color = isSuccess ? "#4caf50" : "#f44336";

    const text = document.createElement("span");
    text.innerText = message;

    toast.appendChild(icon);
    toast.appendChild(text);
    container.appendChild(toast);

    // Auto-remove toast after 3.5 seconds
    setTimeout(function () {
        toast.style.transition = "opacity 0.4s ease-out, transform 0.4s ease-out";
        toast.style.opacity = "0";
        toast.style.transform = "translateY(20px)";
        setTimeout(function () {
            toast.remove();
        }, 400);
    }, 3500);
}

// Helper: Query active session cart items count and update badge
function updateHeaderCartCount() {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Cart/GetCartCount", true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            try {
                var response = JSON.parse(xhr.responseText);
                var badge = document.getElementById("cart-badge");
                if (badge) {
                    badge.innerText = response.count;
                }
            } catch (e) {
                console.error("Lỗi parse dữ liệu giỏ hàng:", e);
            }
        }
    };
    xhr.send();
}
