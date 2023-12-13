using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Domain.Entities
{
    public class BoardEntity
    {
        [Key]
        public Guid BoardId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string[] Statuses { get; set; }
    }
}