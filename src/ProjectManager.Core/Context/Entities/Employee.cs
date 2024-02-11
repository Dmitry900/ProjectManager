using ProjectManager.Core.Context.Entities.Interfaces;

namespace ProjectManager.Core.Context.Entities
{
    public class Employee : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public List<Project> Projects { get; set; }
    }
}