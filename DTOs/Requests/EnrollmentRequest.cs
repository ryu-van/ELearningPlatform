using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class EnrollmentRequest
    {
        [Required]
        public long CourseId { get; set; }
    }
}
