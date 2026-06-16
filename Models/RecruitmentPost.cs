using System.ComponentModel.DataAnnotations;

namespace DATN.Models
{
    public class RecruitmentPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề tuyển dụng là bắt buộc")]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phòng ban là bắt buộc")]
        [StringLength(100)]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Địa điểm làm việc là bắt buộc")]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mức lương là bắt buộc")]
        [StringLength(100)]
        public string SalaryRange { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mô tả công việc là bắt buộc")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yêu cầu ứng viên là bắt buộc")]
        public string Requirements { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quyền lợi được hưởng là bắt buộc")]
        public string Benefits { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
