using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Domain.Entities
{
    public class TaskEntity
    {
        [Key]
        public Guid TaskId { get; set; }
        public Guid BoardId { get; set; }
        public string Name { get; set; }

        public ICollection<RecordEntity> Records { get; set; }
    }
}