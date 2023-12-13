using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Domain.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
    }
}
