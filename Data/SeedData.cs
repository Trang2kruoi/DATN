using Microsoft.EntityFrameworkCore;
using DATN.Models;

namespace DATN.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Ensure the database is created and migrated
                context.Database.Migrate();

                // Look for any categories.
                if (context.Categories.Any())
                {
                    return;   // DB has been seeded
                }

                // 1. Seed Brands
                var apple = new Brand { Name = "Apple", Description = "Thương hiệu công nghệ hàng đầu thế giới từ Mỹ", Logo = "Apple_logo.png" };
                var samsung = new Brand { Name = "Samsung", Description = "Tập đoàn công nghệ đa quốc gia từ Hàn Quốc", Logo = "Samsung_logo.png" };
                var asus = new Brand { Name = "Asus", Description = "Nhà sản xuất phần cứng máy tính và điện tử từ Đài Loan", Logo = "Asus_logo.png" };

                context.Brands.AddRange(apple, samsung, asus);
                context.SaveChanges();

                // 2. Seed Categories
                var dienThoai = new Category { Name = "Điện thoại", Slug = "dien-thoai", Description = "Các dòng điện thoại thông minh mới nhất" };
                var laptop = new Category { Name = "Laptop", Slug = "laptop", Description = "Máy tính xách tay văn phòng, gaming, đồ họa" };
                var mayTinhBang = new Category { Name = "Máy tính bảng", Slug = "may-tinh-bang", Description = "Tablet hỗ trợ làm việc và giải trí" };
                var thietBiAmThanh = new Category { Name = "Thiết bị âm thanh", Slug = "thiet-bi-am-thanh", Description = "Tai nghe, loa bluetooth, soundbar chính hãng" };
                var phuKien = new Category { Name = "Phụ kiện", Slug = "phu-kien", Description = "Cáp sạc, pin dự phòng, ốp lưng, cường lực" };

                context.Categories.AddRange(dienThoai, laptop, mayTinhBang, thietBiAmThanh, phuKien);
                context.SaveChanges();

                // 3. Seed Products (30+ items)
                var products = new List<Product>
                {
                    // === ĐIỆN THOẠI ===
                    new Product
                    {
                        Name = "iPhone 15 Pro Max 256GB",
                        Slug = "iphone-15-pro-max-256gb",
                        SKU = "IP15PM256",
                        Price = 29990000,
                        OriginalPrice = 34990000,
                        Description = "iPhone 15 Pro Max sở hữu thiết kế khung viền Titanium siêu bền, màn hình Dynamic Island ấn tượng, và chip A17 Pro mạnh mẽ nhất hiện nay.",
                        Specifications = "Màn hình: 6.7 inch Super Retina XDR OLED\nChip: Apple A17 Pro 3nm\nCamera sau: Chính 48 MP & Phụ 12 MP, 12 MP\nCamera trước: 12 MP\nRAM: 8 GB\nBộ nhớ trong: 256 GB\nPin: 4422 mAh, Sạc 20W",
                        ImageUrl = "https://images.unsplash.com/photo-1695048133142-1a20484d2569?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 15,
                        Category = dienThoai,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy S24 Ultra 512GB",
                        Slug = "samsung-galaxy-s24-ultra-512gb",
                        SKU = "S24U512",
                        Price = 28490000,
                        OriginalPrice = 33990000,
                        Description = "Samsung Galaxy S24 Ultra mang đến công nghệ Galaxy AI đỉnh cao, bút S-Pen tiện lợi và camera zoom quang học 100x thế hệ mới.",
                        Specifications = "Màn hình: 6.8 inch Dynamic AMOLED 2X 120Hz\nChip: Snapdragon 8 Gen 3 for Galaxy\nCamera sau: 200 MP + 50 MP + 12 MP + 10 MP\nCamera trước: 12 MP\nRAM: 12 GB\nBộ nhớ trong: 512 GB\nPin: 5000 mAh, Sạc 45W",
                        ImageUrl = "https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 12,
                        Category = dienThoai,
                        Brand = samsung
                    },
                    new Product
                    {
                        Name = "iPhone 14 Pro 128GB",
                        Slug = "iphone-14-pro-128gb",
                        SKU = "IP14P128",
                        Price = 22490000,
                        OriginalPrice = 24990000,
                        Description = "iPhone 14 Pro đánh dấu sự xuất hiện của màn hình Dynamic Island, camera 48MP và vi xử lý Apple A16 Bionic mượt mà.",
                        Specifications = "Màn hình: 6.1 inch Super Retina XDR OLED\nChip: Apple A16 Bionic\nCamera sau: 48 MP + 12 MP + 12 MP\nCamera trước: 12 MP\nRAM: 6 GB\nBộ nhớ trong: 128 GB\nPin: 3200 mAh, Sạc 20W",
                        ImageUrl = "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 8,
                        Category = dienThoai,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy Z Fold5 256GB",
                        Slug = "samsung-galaxy-z-fold5-256gb",
                        SKU = "ZFOLD5-256",
                        Price = 31990000,
                        OriginalPrice = 40990000,
                        Description = "Điện thoại gập cao cấp Samsung Galaxy Z Fold5 với bản lề Flex cải tiến mới, thiết kế mỏng nhẹ hơn và cấu hình mạnh mẽ vượt trội.",
                        Specifications = "Màn hình: Chính 7.6 inch & Phụ 6.2 inch Dynamic AMOLED 2X\nChip: Snapdragon 8 Gen 2 for Galaxy\nCamera sau: 50 MP + 12 MP + 10 MP\nCamera trước: 10 MP & 4 MP\nRAM: 12 GB\nBộ nhớ trong: 256 GB\nPin: 4400 mAh, Sạc 25W",
                        ImageUrl = "https://images.unsplash.com/photo-1610945415295-d9bff066e50d?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 6,
                        Category = dienThoai,
                        Brand = samsung
                    },
                    new Product
                    {
                        Name = "iPhone 13 128GB",
                        Slug = "iphone-13-128gb",
                        SKU = "IP13-128",
                        Price = 13990000,
                        OriginalPrice = 16990000,
                        Description = "iPhone 13 là sự lựa chọn hợp lý nhất với hiệu năng ổn định từ A15 Bionic, camera kép chéo độc đáo và thời lượng pin được nâng cấp.",
                        Specifications = "Màn hình: 6.1 inch Super Retina XDR OLED\nChip: Apple A15 Bionic\nCamera sau: 2 camera 12 MP\nCamera trước: 12 MP\nRAM: 4 GB\nBộ nhớ trong: 128 GB\nPin: 3240 mAh, Sạc 20W",
                        ImageUrl = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 25,
                        Category = dienThoai,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy A55 5G 128GB",
                        Slug = "samsung-galaxy-a55-5g-128gb",
                        SKU = "GALA55-128",
                        Price = 9690000,
                        OriginalPrice = 9990000,
                        Description = "Samsung Galaxy A55 5G sở hữu thiết kế khung kim loại cao cấp, camera chống rung OIS cực chất và khả năng kháng nước IP67 chuẩn chỉ.",
                        Specifications = "Màn hình: 6.6 inch Super AMOLED 120Hz\nChip: Exynos 1480 8 nhân\nCamera sau: 50 MP + 12 MP + 5 MP\nCamera trước: 32 MP\nRAM: 8 GB\nBộ nhớ trong: 128 GB\nPin: 5000 mAh, Sạc 25W",
                        ImageUrl = "https://images.unsplash.com/photo-1580910051074-3eb694886505?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = false,
                        IsBestSeller = false,
                        Stock = 30,
                        Category = dienThoai,
                        Brand = samsung
                    },

                    // === LAPTOP ===
                    new Product
                    {
                        Name = "MacBook Air M2 8GB 256GB",
                        Slug = "macbook-air-m2-8gb-256gb",
                        SKU = "MBAIRM2-256",
                        Price = 24990000,
                        OriginalPrice = 27990000,
                        Description = "MacBook Air M2 mang diện mạo hoàn toàn mới siêu mỏng, hiệu năng vượt trội từ vi xử lý M2 thế hệ mới và màn hình Liquid Retina tuyệt đẹp.",
                        Specifications = "Màn hình: 13.6 inch Liquid Retina IPS\nChip: Apple M2 8 nhân CPU & 8 nhân GPU\nRAM: 8 GB\nSSD: 256 GB\nCổng kết nối: 2 x Thunderbolt 3, sạc MagSafe 3, Jack 3.5mm\nTrọng lượng: 1.24 kg",
                        ImageUrl = "https://images.unsplash.com/photo-1611186871348-b1ce696e52c9?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 10,
                        Category = laptop,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "MacBook Pro M3 Pro 18GB 512GB",
                        Slug = "macbook-pro-m3-pro-18gb-512gb",
                        SKU = "MBPROM3P-512",
                        Price = 49990000,
                        OriginalPrice = 54990000,
                        Description = "Dành cho lập trình viên và nhà làm phim chuyên nghiệp, MacBook Pro M3 Pro cung cấp sức mạnh đồ họa và đa nhiệm khủng khiếp cùng thời lượng pin lên đến 22 giờ.",
                        Specifications = "Màn hình: 14.2 inch Liquid Retina XDR 120Hz\nChip: Apple M3 Pro 12 nhân CPU & 18 nhân GPU\nRAM: 18 GB\nSSD: 512 GB\nHĐH: macOS\nTrọng lượng: 1.61 kg",
                        ImageUrl = "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = false,
                        IsBestSeller = false,
                        Stock = 5,
                        Category = laptop,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "ASUS ROG Zephyrus G14 Gaming",
                        Slug = "asus-rog-zephyrus-g14-gaming",
                        SKU = "ZEPHYRUS-G14",
                        Price = 38990000,
                        OriginalPrice = 42990000,
                        Description = "Chiếc laptop gaming mỏng nhẹ bậc nhất thế giới, thiết kế sang trọng đẳng cấp với cấu hình chip AMD Ryzen 9 và card đồ họa RTX 4060 cực mạnh.",
                        Specifications = "Màn hình: 14 inch QHD+ 165Hz ROG Nebula\nChip: AMD Ryzen 9 7940HS\nRAM: 16 GB DDR5\nSSD: 512 GB PCIe 4.0\nCard màn hình: NVIDIA RTX 4060 8GB\nTrọng lượng: 1.65 kg",
                        ImageUrl = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 7,
                        Category = laptop,
                        Brand = asus
                    },
                    new Product
                    {
                        Name = "ASUS TUF Gaming A15 Ryzen 7",
                        Slug = "asus-tuf-gaming-a15-ryzen-7",
                        SKU = "TUF-A15-R7",
                        Price = 20490000,
                        OriginalPrice = 22990000,
                        Description = "ASUS TUF Gaming A15 bền bỉ chuẩn quân đội, sở hữu hiệu năng mạnh mẽ từ CPU Ryzen 7 và GPU RTX 3050 chiến mượt mọi game esport phổ biến.",
                        Specifications = "Màn hình: 15.6 inch FHD IPS 144Hz\nChip: AMD Ryzen 7 7735HS\nRAM: 8 GB DDR5\nSSD: 512 GB NVMe\nCard màn hình: NVIDIA RTX 3050 4GB\nTrọng lượng: 2.2 kg",
                        ImageUrl = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 15,
                        Category = laptop,
                        Brand = asus
                    },
                    new Product
                    {
                        Name = "ASUS Zenbook 14 OLED Ultra 5",
                        Slug = "asus-zenbook-14-oled-ultra-5",
                        SKU = "ZENBOOK-14-U5",
                        Price = 25990000,
                        OriginalPrice = 27990000,
                        Description = "ASUS Zenbook 14 OLED là biểu tượng của sự tinh tế với màn hình OLED 3K siêu sắc nét, chip Intel Core Ultra 5 tích hợp AI tối tân.",
                        Specifications = "Màn hình: 14 inch OLED 3K 120Hz\nChip: Intel Core Ultra 5 125H\nRAM: 16 GB LPDDR5X\nSSD: 512 GB PCIe 4.0\nCard đồ họa: Intel Arc Graphics\nTrọng lượng: 1.2 kg",
                        ImageUrl = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = false,
                        IsBestSeller = true,
                        Stock = 10,
                        Category = laptop,
                        Brand = asus
                    },

                    // === MÁY TÍNH BẢNG ===
                    new Product
                    {
                        Name = "iPad Pro M4 11 inch Wifi 256GB",
                        Slug = "ipad-pro-m4-11-inch-wifi-256gb",
                        SKU = "IPADPROM4-11",
                        Price = 28990000,
                        OriginalPrice = 30990000,
                        Description = "iPad Pro M4 mới siêu mỏng, sở hữu màn hình Tandem OLED cực kỳ rực rỡ và sức mạnh xử lý vượt trội từ chip M4 thế hệ mới nhất.",
                        Specifications = "Màn hình: 11 inch Tandem OLED 120Hz\nChip: Apple M4 9 nhân\nRAM: 8 GB\nBộ nhớ trong: 256 GB\nCamera sau: 12 MP & Cảm biến LiDAR\nTrọng lượng: 444g",
                        ImageUrl = "https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 12,
                        Category = mayTinhBang,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "iPad Air 6 M2 11 inch 128GB",
                        Slug = "ipad-air-6-m2-11-inch-128gb",
                        SKU = "IPADAIRM2-11",
                        Price = 16490000,
                        OriginalPrice = 17990000,
                        Description = "iPad Air thế hệ 6 được tích hợp chip M2 thần tốc, hỗ trợ Apple Pencil Pro và mang lại sự linh hoạt tuyệt vời cho công việc sáng tạo.",
                        Specifications = "Màn hình: 11 inch Liquid Retina IPS\nChip: Apple M2 8 nhân\nRAM: 8 GB\nBộ nhớ trong: 128 GB\nCamera: Trước & Sau 12 MP\nHĐH: iPadOS",
                        ImageUrl = "https://images.unsplash.com/photo-1561154464-82e9adf32764?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 18,
                        Category = mayTinhBang,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy Tab S9 Ultra 256GB",
                        Slug = "samsung-galaxy-tab-s9-ultra-256gb",
                        SKU = "TABS9U-256",
                        Price = 25990000,
                        OriginalPrice = 32990000,
                        Description = "Chiếc tablet khổng lồ 14.6 inch của Samsung đi kèm bút S-Pen, chống nước bụi IP68, đáp ứng hoàn hảo cho nhu cầu vẽ đồ họa chuyên nghiệp.",
                        Specifications = "Màn hình: 14.6 inch Dynamic AMOLED 2X 120Hz\nChip: Snapdragon 8 Gen 2 for Galaxy\nRAM: 12 GB\nBộ nhớ trong: 256 GB\nCamera sau: 13 MP & 8 MP\nCamera trước: Cặp camera 12 MP\nPin: 11200 mAh, Sạc 45W",
                        ImageUrl = "https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 6,
                        Category = mayTinhBang,
                        Brand = samsung
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy Tab A9+ Wifi",
                        Slug = "samsung-galaxy-tab-a9-plus-wifi",
                        SKU = "TABA9PLUS-WF",
                        Price = 5490000,
                        OriginalPrice = 5990000,
                        Description = "Galaxy Tab A9+ là dòng máy tính bảng phổ thông phù hợp cho học sinh, giải trí gia đình với màn hình lớn và hệ thống âm thanh 4 loa.",
                        Specifications = "Màn hình: 11 inch TFT LCD 90Hz\nChip: Snapdragon 695 5G\nRAM: 4 GB\nBộ nhớ trong: 64 GB\nCamera sau: 8 MP\nCamera trước: 5 MP\nPin: 7040 mAh",
                        ImageUrl = "https://images.unsplash.com/photo-1527690787265-a83d3e8e1694?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = false,
                        IsBestSeller = true,
                        Stock = 20,
                        Category = mayTinhBang,
                        Brand = samsung
                    },

                    // === THIẾT BỊ ÂM THANH ===
                    new Product
                    {
                        Name = "Tai nghe AirPods Pro Gen 2 USB-C",
                        Slug = "airpods-pro-gen-2-usb-c",
                        SKU = "AIRPODSPRO2-C",
                        Price = 5690000,
                        OriginalPrice = 6190000,
                        Description = "AirPods Pro Gen 2 nay có thêm cổng USB-C tiện lợi, công nghệ khử tiếng ồn chủ động ANC tăng gấp đôi hiệu quả cách âm.",
                        Specifications = "Chip: Apple H2\nKết nối: Bluetooth 5.3, USB-C trên hộp sạc\nThời lượng pin: Lên đến 6 giờ (tắt ANC là 30 giờ kèm hộp sạc)\nKháng nước: IP54 (cả tai nghe và hộp)\nTính năng: Chống ồn chủ động (ANC), Xuyên âm thích ứng, Âm thanh không gian",
                        ImageUrl = "https://images.unsplash.com/photo-1588449668338-d134ac2c384f?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 40,
                        Category = thietBiAmThanh,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Tai nghe chụp tai AirPods Max",
                        Slug = "tai-nghe-airpods-max",
                        SKU = "AIRPODSMAX",
                        Price = 11990000,
                        OriginalPrice = 13990000,
                        Description = "Tai nghe Over-ear cao cấp từ Apple mang lại chất âm Hi-Fi độ chi tiết cực cao, thiết kế khung kim loại sang trọng ôm sát tai thoải mái.",
                        Specifications = "Chip: 2 chip Apple H1 (mỗi bên một chip)\nKết nối: Bluetooth 5.0\nThời lượng pin: 20 giờ sử dụng\nTrọng lượng: 384.8g\nTính năng: Chống ồn chủ động, Xuyên âm, EQ thích ứng",
                        ImageUrl = "https://images.unsplash.com/photo-1608156639585-b3a032ef9689?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 15,
                        Category = thietBiAmThanh,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy Buds2 Pro",
                        Slug = "samsung-galaxy-buds2-pro",
                        SKU = "GABUDS2PRO",
                        Price = 3290000,
                        OriginalPrice = 4990000,
                        Description = "Buds2 Pro mang lại trải nghiệm âm thanh vòm 360 độ sống động chân thực, thiết kế hạt đậu nhỏ gọn và kháng nước IPX7.",
                        Specifications = "Kết nối: Bluetooth 5.3\nThời lượng pin: 5 giờ liên tục (hộp sạc thêm 18 giờ)\nKháng nước: IPX7\nTính năng: Chống ồn chủ động thông minh ANC, Phát hiện giọng nói nói chuyện tự động chuyển xuyên âm",
                        ImageUrl = "https://images.unsplash.com/photo-1590658268037-6bf12165a8df?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 35,
                        Category = thietBiAmThanh,
                        Brand = samsung
                    },
                    new Product
                    {
                        Name = "Loa Bluetooth ASUS ROG Cetra True Wireless",
                        Slug = "asus-rog-cetra-true-wireless",
                        SKU = "ROGCETRA-TW",
                        Price = 1790000,
                        OriginalPrice = 2290000,
                        Description = "Tai nghe true wireless chuyên game với độ trễ cực thấp, chống ồn chủ động ANC tích hợp và đèn LED RGB bắt mắt chuẩn ROG.",
                        Specifications = "Kết nối: Bluetooth 5.0\nĐộ trễ: 60ms (Game Mode)\nThời lượng pin: Tối đa 27 giờ (kèm hộp)\nKháng nước: IPX4\nTương thích: PC, Mac, Nintendo Switch, Mobile",
                        ImageUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 18,
                        Category = thietBiAmThanh,
                        Brand = asus
                    },

                    // === PHỤ KIỆN ===
                    new Product
                    {
                        Name = "Sạc Apple USB-C 20W chính hãng",
                        Slug = "sac-apple-usb-c-20w",
                        SKU = "AP-20W-CHARGER",
                        Price = 550000,
                        OriginalPrice = 690000,
                        Description = "Củ sạc nhanh 20W chính hãng Apple cung cấp dòng điện sạc nhanh an toàn, ổn định cho iPhone và iPad.",
                        Specifications = "Công suất: 20W\nCổng ra: USB-C\nHỗ trợ sạc nhanh: Power Delivery (PD)\nTương thích: iPhone 8 trở lên, iPad Pro/Air/Mini",
                        ImageUrl = "https://images.unsplash.com/photo-1583863788434-e58a36330cf0?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 100,
                        Category = phuKien,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Bút Apple Pencil 2",
                        Slug = "but-apple-pencil-2",
                        SKU = "APENCIL2",
                        Price = 2990000,
                        OriginalPrice = 3490000,
                        Description = "Apple Pencil 2 hỗ trợ vẽ phác thảo chính xác đến từng pixel, sạc không dây từ tính trực tiếp khi hít vào cạnh iPad Pro/Air.",
                        Specifications = "Kết nối: Bluetooth\nSạc: Không dây từ tính trực tiếp từ iPad\nTương thích: iPad Pro 11/12.9 inch, iPad Air 4/5, iPad Mini 6",
                        ImageUrl = "https://images.unsplash.com/photo-1585776245991-cf89dd7fc73a?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = false,
                        IsBestSeller = false,
                        Stock = 40,
                        Category = phuKien,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Cáp Sạc Nhanh Samsung Type-C To Type-C 1.8m",
                        Slug = "cap-sac-nhanh-samsung-type-c-to-type-c-1-8m",
                        SKU = "SS-CC-1.8M",
                        Price = 190000,
                        OriginalPrice = 250000,
                        Description = "Cáp sạc chính hãng Samsung dài 1.8m hỗ trợ truyền dữ liệu tốc độ cao và sạc siêu nhanh công suất tối đa 60W.",
                        Specifications = "Độ dài: 1.8 mét\nChuẩn giao tiếp: Type-C sang Type-C\nCông suất hỗ trợ: 60W (20V - 3A)\nMàu sắc: Đen / Trắng",
                        ImageUrl = "https://images.unsplash.com/photo-1543269664-76bc3997d9ea?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 150,
                        Category = phuKien,
                        Brand = samsung
                    },
                    new Product
                    {
                        Name = "Chuột chơi game ASUS ROG Gladius III Wireless",
                        Slug = "chuot-asus-rog-gladius-iii-wireless",
                        SKU = "GLADIUS-III-WL",
                        Price = 2190000,
                        OriginalPrice = 2590000,
                        Description = "Chuột gaming công thái học không dây kết nối 3 chế độ (RF 2.4GHz, Bluetooth, có dây) cảm biến 19,000 DPI siêu nhạy.",
                        Specifications = "Độ nhạy cảm biến: 19,000 DPI (được tối ưu hóa lên 26,000 DPI)\nKết nối: Bluetooth / Wireless 2.4Ghz / USB Wired\nSwitch: ROG Micro Switch 70 triệu click\nTrọng lượng: 89g (không cáp)",
                        ImageUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 22,
                        Category = phuKien,
                        Brand = asus
                    },

                    // === THÊM SẢN PHẨM KHÁC ĐỂ ĐẠT 30+ SẢN PHẨM ===
                    new Product
                    {
                        Name = "iPhone 15 Plus 128GB",
                        Slug = "iphone-15-plus-128gb",
                        SKU = "IP15PLUS-128",
                        Price = 22990000,
                        OriginalPrice = 25990000,
                        Description = "iPhone 15 Plus trang bị Dynamic Island, Camera 48MP và pin trâu ấn tượng, kết hợp màn hình 6.7 inch siêu rộng rãi thoải mái.",
                        Specifications = "Màn hình: 6.7 inch Super Retina XDR OLED\nChip: Apple A16 Bionic\nCamera sau: 48 MP + 12 MP\nRAM: 6 GB\nROM: 128 GB\nPin: 4383 mAh",
                        ImageUrl = "https://images.unsplash.com/photo-1695048133142-1a20484d2569?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 14,
                        Category = dienThoai,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "iPhone 15 128GB",
                        Slug = "iphone-15-128gb",
                        SKU = "IP15-128",
                        Price = 19990000,
                        OriginalPrice = 22990000,
                        Description = "iPhone 15 phiên bản tiêu chuẩn sở hữu cổng kết nối USB-C mới, mặt lưng kính pha màu sang trọng cùng cụm camera sắc nét.",
                        Specifications = "Màn hình: 6.1 inch OLED\nChip: Apple A16 Bionic\nCamera: 48 MP + 12 MP\nRAM: 6 GB\nROM: 128 GB",
                        ImageUrl = "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = false,
                        IsBestSeller = true,
                        Stock = 20,
                        Category = dienThoai,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy S24 FE 128GB",
                        Slug = "samsung-galaxy-s24-fe-128gb",
                        SKU = "S24FE-128",
                        Price = 14990000,
                        OriginalPrice = 16990000,
                        Description = "Phiên bản Fan Edition của dòng S24 cao cấp đem lại hiệu năng mạnh mẽ cận flagship với mức giá cực kỳ phải chăng.",
                        Specifications = "Màn hình: 6.4 inch Dynamic AMOLED 2X 120Hz\nChip: Exynos 2400e\nRAM: 8 GB\nROM: 128 GB\nCamera: 50 MP + 12 MP + 8 MP",
                        ImageUrl = "https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 17,
                        Category = dienThoai,
                        Brand = samsung
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy M54 5G",
                        Slug = "samsung-galaxy-m54-5g",
                        SKU = "M545G",
                        Price = 8490000,
                        OriginalPrice = 10490000,
                        Description = "Điện thoại tầm trung pin siêu trâu 6000mAh và camera 108MP chống rung OIS chuyên nghiệp từ Samsung.",
                        Specifications = "Màn hình: 6.7 inch Super AMOLED Plus 120Hz\nChip: Exynos 1380\nRAM: 8 GB\nROM: 256 GB\nPin: 6000 mAh",
                        ImageUrl = "https://images.unsplash.com/photo-1580910051074-3eb694886505?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 25,
                        Category = dienThoai,
                        Brand = samsung
                    },
                    new Product
                    {
                        Name = "ASUS Vivobook 14 X1404VA",
                        Slug = "asus-vivobook-14-x1404va",
                        SKU = "VIVO-X1404",
                        Price = 12990000,
                        OriginalPrice = 14990000,
                        Description = "Chiếc laptop văn phòng, học tập quốc dân thiết kế thanh lịch gọn nhẹ, trang bị ổ cứng SSD tốc độ cao và màn hình IPS sắc nét.",
                        Specifications = "Màn hình: 14 inch Full HD IPS\nChip: Intel Core i3-1315U\nRAM: 8 GB DDR4\nSSD: 512 GB NVMe\nHĐH: Windows 11\nTrọng lượng: 1.4 kg",
                        ImageUrl = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 30,
                        Category = laptop,
                        Brand = asus
                    },
                    new Product
                    {
                        Name = "ASUS ROG Strix G16 Gaming",
                        Slug = "asus-rog-strix-g16-gaming",
                        SKU = "ROG-STRIX-G16",
                        Price = 33990000,
                        OriginalPrice = 36990000,
                        Description = "Laptop gaming chuyên nghiệp dòng Strix mạnh mẽ với card đồ họa RTX 4050, hệ thống tản nhiệt thông minh 3 quạt siêu mát.",
                        Specifications = "Màn hình: 16 inch FHD+ IPS 165Hz\nChip: Intel Core i7-13650HX\nRAM: 16 GB DDR5\nSSD: 512 GB PCIe 4.0\nCard màn hình: NVIDIA RTX 4050 6GB\nTrọng lượng: 2.5 kg",
                        ImageUrl = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = true,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 5,
                        Category = laptop,
                        Brand = asus
                    },
                    new Product
                    {
                        Name = "iPad 10.9 inch Wifi 64GB (Gen 10)",
                        Slug = "ipad-gen-10-64gb",
                        SKU = "IPAD-GEN10-64",
                        Price = 9290000,
                        OriginalPrice = 10990000,
                        Description = "iPad Gen 10 lột xác với thiết kế vuông vức tràn viền hiện đại, nhiều màu sắc năng động cá tính, cổng USB-C tiện lợi.",
                        Specifications = "Màn hình: 10.9 inch Liquid Retina IPS\nChip: Apple A14 Bionic\nRAM: 4 GB\nROM: 64 GB\nCamera trước & sau: 12 MP",
                        ImageUrl = "https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = true,
                        Stock = 45,
                        Category = mayTinhBang,
                        Brand = apple
                    },
                    new Product
                    {
                        Name = "Tai nghe chụp tai ASUS ROG Strix Go Core",
                        Slug = "asus-rog-strix-go-core",
                        SKU = "ROGSTRIX-GO",
                        Price = 1690000,
                        OriginalPrice = 1990000,
                        Description = "Tai nghe gaming tương thích đa nền tảng mang lại âm thanh chơi game đắm chìm và cảm giác đeo êm ái nhẹ nhàng.",
                        Specifications = "Kiểu tai nghe: Chụp tai Over-ear\nKết nối: Jack 3.5mm\nMàng loa: ASUS Essence 40mm\nTương thích: PC, Mac, PS5, Xbox Series X/S, Nintendo Switch",
                        ImageUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = false,
                        IsBestSeller = false,
                        Stock = 20,
                        Category = thietBiAmThanh,
                        Brand = asus
                    },
                    new Product
                    {
                        Name = "Bàn phím cơ ASUS ROG Strix Scope II",
                        Slug = "ban-phim-asus-rog-strix-scope-ii",
                        SKU = "SCOPE-II-KB",
                        Price = 2890000,
                        OriginalPrice = 3290000,
                        Description = "Bàn phím cơ gaming fullsize ROG Scope II sử dụng switch cơ học ROG độc quyền gõ siêu mượt và êm tai.",
                        Specifications = "Loại bàn phím: Bàn phím cơ\nSwitch: ROG NX Mechanical (Red/Brown/Blue)\nKết nối: Có dây USB\nĐèn nền: RGB Aura Sync từng phím\nĐệm kê tay: Có sẵn đi kèm",
                        ImageUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?q=80&w=600&auto=format&fit=crop",
                        IsFeatured = false,
                        IsSale = true,
                        IsBestSeller = false,
                        Stock = 15,
                        Category = phuKien,
                        Brand = asus
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();

                // 4. Seed Articles (3 items)
                var articles = new List<Article>
                {
                    new Article
                    {
                        Title = "Đánh giá chi tiết iPhone 15 Pro Max sau 6 tháng sử dụng",
                        Slug = "danh-gia-chi-tiet-iphone-15-pro-max",
                        Summary = "Liệu khung viền Titanium và con chip A17 Pro có thực sự đáng tiền như lời đồn? Cùng tìm hiểu qua bài viết dưới đây.",
                        Content = "<p>Sau hơn 6 tháng ra mắt, iPhone 15 Pro Max vẫn đang là chiếc smartphone cao cấp được săn đón hàng đầu. Trải nghiệm thực tế cho thấy khung viền Titanium thực sự mang lại cảm giác cầm nắm nhẹ nhàng hơn hẳn so với thép không gỉ trên các thế hệ trước.</p><p>Về hiệu năng, chip A17 Pro đáp ứng mượt mà tất cả các game đồ họa nặng nhất hiện nay như Genshin Impact ở mức thiết lập đồ họa cao nhất mà không bị quá nhiệt. Camera zoom 5x cũng hoạt động vô cùng hiệu quả trong việc bắt trọn những khoảnh khắc ở xa một cách sắc nét nhất.</p><p>Tóm lại, nếu bạn đang tìm kiếm một chiếc điện thoại bền bỉ, hiệu năng đỉnh cao và camera quay chụp chuyên nghiệp thì iPhone 15 Pro Max vẫn là lựa chọn số một ở thời điểm hiện tại.</p>",
                        ImageUrl = "https://images.unsplash.com/photo-1695048133142-1a20484d2569?q=80&w=600&auto=format&fit=crop",
                        CreatedDate = DateTime.Now.AddDays(-10)
                    },
                    new Article
                    {
                        Title = "Top 5 Laptop Gaming đáng mua nhất cho sinh viên 2026",
                        Slug = "top-5-laptop-gaming-sinh-vien-2026",
                        Summary = "Tổng hợp danh sách những mẫu laptop gaming mỏng nhẹ, cấu hình cao và có mức giá vô cùng hợp lý dành cho học sinh, sinh viên.",
                        Content = "<p>Mùa tựu trường đang đến gần, việc chọn cho mình một chiếc laptop vừa phục vụ học tập vừa có thể giải trí chiến game mượt mà là nhu cầu của rất nhiều bạn sinh viên. Dưới đây là 5 đại diện xuất sắc nhất năm nay:</p><ul><li><strong>ASUS TUF Gaming A15:</strong> Lựa chọn hàng đầu về độ bền bỉ quân đội và giá thành hợp lý.</li><li><strong>ASUS ROG Zephyrus G14:</strong> Thích hợp cho các bạn thích mỏng nhẹ, sang chảnh nhưng cấu hình vẫn khủng.</li><li><strong>Lenovo Legion 5:</strong> Màn hình đẹp, tản nhiệt tốt nhất phân khúc.</li><li><strong>Acer Nitro V:</strong> Giá rẻ nhất, dễ tiếp cận cho sinh viên năm nhất.</li><li><strong>MSI Cyborg 15:</strong> Thiết kế futuristic độc đáo.</li></ul><p>Hãy tùy thuộc vào ngân sách và ngành học của mình để chọn chiếc máy phù hợp nhất nhé!</p>",
                        ImageUrl = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?q=80&w=600&auto=format&fit=crop",
                        CreatedDate = DateTime.Now.AddDays(-5)
                    },
                    new Article
                    {
                        Title = "Bí quyết chọn tai nghe chống ồn phù hợp với nhu cầu",
                        Slug = "bi-quyet-chon-tai-nghe-chong-on",
                        Summary = "Hướng dẫn phân biệt chống ồn chủ động ANC và chống ồn thụ động, giúp bạn chọn được chiếc tai nghe ưng ý nhất.",
                        Content = "<p>Trong cuộc sống hiện đại đầy tiếng ồn, chiếc tai nghe chống ồn chủ động (ANC) đã trở thành vật bất ly thân của nhiều người. Tuy nhiên, không phải ai cũng biết cách chọn tai nghe phù hợp với nhu cầu sử dụng của mình.</p><p>Nếu bạn là người thường xuyên di chuyển bằng máy bay, tàu xe hoặc làm việc trong văn phòng mở, tai nghe Over-ear (chụp tai) như AirPods Max sẽ mang lại hiệu quả cách âm tốt nhất. Ngược lại, nếu cần sự gọn nhẹ, linh hoạt khi tập thể thao hay đi bộ, các dòng tai nghe True Wireless như AirPods Pro Gen 2 hay Samsung Galaxy Buds2 Pro là lựa chọn lý tưởng.</p><p>Hãy lưu ý thời lượng pin và khả năng chống nước khi mua tai nghe để có trải nghiệm tốt nhất!</p>",
                        ImageUrl = "https://images.unsplash.com/photo-1588449668338-d134ac2c384f?q=80&w=600&auto=format&fit=crop",
                        CreatedDate = DateTime.Now.AddDays(-2)
                    }
                };

                context.Articles.AddRange(articles);
                context.SaveChanges();

                // 5. Seed Recruitment Posts (3 items)
                var recruitments = new List<RecruitmentPost>
                {
                    new RecruitmentPost
                    {
                        Title = "Lập Trình Viên .NET (C#, ASP.NET Core)",
                        Department = "Phòng Phát triển Công nghệ",
                        Location = "TP. Hồ Chí Minh",
                        SalaryRange = "15,000,000 - 30,000,000 VNĐ",
                        Description = "Chúng tôi đang tìm kiếm các kỹ sư phần mềm .NET tài năng để tham gia phát triển các dự án e-commerce quy mô lớn, tối ưu hóa hệ thống Backend phục vụ hàng triệu người dùng.",
                        Requirements = "- Tối thiểu 2 năm kinh nghiệm lập trình C#, .NET Core / ASP.NET MVC.\n- Thành thạo Entity Framework Core, SQL Server hoặc PostgreSQL/SQLite.\n- Có kinh nghiệm thiết kế RESTful APIs, hiểu biết về Microservices là lợi thế.\n- Khả năng làm việc nhóm tốt, tư duy logic nhạy bén.",
                        Benefits = "- Lương tháng 13 + thưởng hiệu quả công việc cuối năm.\n- Đóng BHXH, BHYT đầy đủ theo quy định pháp luật.\n- Môi trường làm việc trẻ trung, năng động, được cấp laptop Macbook/ASUS ROG xịn sò.\n- Du lịch công ty 2 lần/năm.",
                        Deadline = DateTime.Now.AddMonths(1)
                    },
                    new RecruitmentPost
                    {
                        Title = "Chuyên Viên Tư Vấn Bán Hàng Showroom",
                        Department = "Phòng Kinh doanh",
                        Location = "TP. Hồ Chí Minh",
                        SalaryRange = "8,000,000 - 15,000,000 VNĐ (Lương cứng + Hoa hồng)",
                        Description = "Chào đón khách hàng tại showroom, tư vấn các sản phẩm công nghệ (Điện thoại, Laptop, Phụ kiện) phù hợp với nhu cầu của khách hàng và chốt đơn hàng.",
                        Requirements = "- Nam/Nữ tuổi từ 18 - 28, ngoại hình ưa nhìn, giọng nói dễ nghe.\n- Đam mê và có hiểu biết cơ bản về các sản phẩm công nghệ là lợi thế lớn.\n- Kỹ năng giao tiếp, thuyết phục tốt.\n- Nhiệt tình, có trách nhiệm cao trong công việc.",
                        Benefits = "- Được đào tạo bài bản về kỹ năng bán hàng và kiến thức sản phẩm công nghệ.\n- Thu nhập hấp dẫn dựa trên doanh số bán được (không giới hạn).\n- Cơ hội thăng tiến lên Trưởng nhóm, Cửa hàng trưởng sau 6 tháng - 1 năm.\n- Hưởng ưu đãi mua hàng nội bộ với giá gốc.",
                        Deadline = DateTime.Now.AddMonths(1)
                    },
                    new RecruitmentPost
                    {
                        Title = "Nhà Thiết Kế UI/UX Sản Phẩm E-Commerce",
                        Department = "Phòng Thiết kế Sản phẩm",
                        Location = "TP. Hồ Chí Minh",
                        SalaryRange = "12,000,000 - 22,000,000 VNĐ",
                        Description = "Nghiên cứu trải nghiệm người dùng, vẽ wireframes và thiết kế giao diện (UI) cho hệ thống Website, Mobile App của chuỗi cửa hàng thương mại điện tử.",
                        Requirements = "- Ít nhất 1 năm kinh nghiệm thiết kế UI/UX trên Figma, Adobe XD.\n- Có portfolio đính kèm thể hiện tư duy thiết kế hiện đại, sạch sẽ và lấy người dùng làm trung tâm.\n- Hiểu biết cơ bản về HTML/CSS để phối hợp tốt với đội ngũ Front-end.\n- Sáng tạo, tỉ mỉ và ham học hỏi công nghệ mới.",
                        Benefits = "- Giờ làm việc linh hoạt, hỗ trợ làm Remote bán phần.\n- Xét tăng lương định kỳ 2 lần/năm.\n- Tham gia các khóa học nâng cao kỹ năng chuyên môn do công ty đài thọ.\n- Được làm việc cùng đội ngũ Tech lead nhiều kinh nghiệm.",
                        Deadline = DateTime.Now.AddMonths(1)
                    }
                };

                context.RecruitmentPosts.AddRange(recruitments);
                context.SaveChanges();
            }
        }
    }
}
