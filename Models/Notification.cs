using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // === Người nhận thông báo ===
        [Required]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        // === Nội dung thông báo ===
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        // === Loại thông báo (Info / Success / Warning / Error / Reminder) ===
        [MaxLength(50)]
        public string? Type { get; set; }

        // === Nhóm thông báo (Course / Payment / Exam / System ...) ===
        [MaxLength(50)]
        public string? Category { get; set; }

        // === Thực thể liên quan (CourseId, ExamId, OrderId, ...) ===
        [MaxLength(50)]
        public string? RelatedEntityType { get; set; }

        public long? RelatedEntityId { get; set; }

        // === Link hành động (nếu người dùng nhấn vào thông báo) ===
        [MaxLength(500)]
        public string? ActionUrl { get; set; }

        // === Trạng thái đọc ===
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }

        // === Trạng thái gửi ===
        public bool IsSent { get; set; } = false;
        public DateTime? SentAt { get; set; }

        // === Thời gian tạo ===
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
