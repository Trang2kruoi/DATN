using System.ComponentModel.DataAnnotations;

namespace DATN.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên thương hiệu là bắt buộc")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? Logo { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
