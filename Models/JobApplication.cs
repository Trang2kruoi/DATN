using System.ComponentModel.DataAnnotations;

namespace DATN.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public int RecruitmentPostId { get; set; }

        public RecruitmentPost? RecruitmentPost { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Link CV hoặc thông tin CV là bắt buộc")]
        [StringLength(1000)]
        public string CVUrlOrText { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? CoverLetter { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.Now;
    }
}
