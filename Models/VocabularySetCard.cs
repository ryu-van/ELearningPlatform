using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("VocabularySetCards")]
    public class VocabularySetCard
    {
        [Key, Column(Order = 0)]
        public long SetId { get; set; }

        [ForeignKey(nameof(SetId))]
        public VocabularySet? VocabularySet { get; set; }

        [Key, Column(Order = 1)]
        public long CardId { get; set; }

        [ForeignKey(nameof(CardId))]
        public VocabularyCard? VocabularyCard { get; set; }

        public int OrderNo { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
