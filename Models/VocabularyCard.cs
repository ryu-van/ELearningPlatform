using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("VocabularyCards")]
    public class VocabularyCard : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Term { get; set; } = string.Empty;

        [Required]
        public string Definition { get; set; } = string.Empty;

        public string? Example { get; set; }

        [MaxLength(100)]
        public string? IPA { get; set; }

        [MaxLength(500)]
        public string? AudioUrl { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [MaxLength(50)]
        public string? PartOfSpeech { get; set; }

        [MaxLength(500)]
        public string? Synonyms { get; set; }

        [MaxLength(500)]
        public string? Antonyms { get; set; }

        [MaxLength(20)]
        public string? DifficultyLevel { get; set; }

        [MaxLength(20)]
        public string? UsageFrequency { get; set; } 

        public bool IsActive { get; set; } = true;
    }
}
