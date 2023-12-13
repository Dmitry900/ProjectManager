using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Domain.Entities
{
    public class TaskEntity
    {
        [Key]
        public Guid TaskId { get; set; }
    }
}
