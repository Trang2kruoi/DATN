using System.ComponentModel.DataAnnotations;

namespace DATN.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề bài viết là bắt buộc")]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Slug { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tóm tắt bài viết là bắt buộc")]
        [StringLength(500)]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nội dung bài viết là bắt buộc")]
        public string Content { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
