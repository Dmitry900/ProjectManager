using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Domain.Entities
{
    public class RecordEntity
    {
        [Key]
        public Guid RecordId { get; set; }
        [ForeignKey(nameof(TaskEntity))]
        public Guid TaskId { get; set; }
        public string Text { get; set; }
        public RecordType Type { get; set; }
    }

    public enum RecordType
    {
        Text,
        Todo
    }
}