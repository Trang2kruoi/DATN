using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Slug { get; set; } = string.Empty;

        [Required(ErrorMessage = "SKU là bắt buộc")]
        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? OriginalPrice { get; set; }

        public string? Description { get; set; }

        public string? Specifications { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsSale { get; set; }

        public bool IsBestSeller { get; set; }

        [Required(ErrorMessage = "Số lượng tồn kho là bắt buộc")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required(ErrorMessage = "Thương hiệu là bắt buộc")]
        public int BrandId { get; set; }

        public Brand? Brand { get; set; }
    }
}
