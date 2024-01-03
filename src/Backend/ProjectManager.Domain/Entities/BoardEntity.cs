using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Domain.Entities
{
    public class BoardEntity
    {
        [Key]
        public Guid BoardId { get; set; }
        [ForeignKey(nameof(UserEntity))]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<string> Statuses { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}