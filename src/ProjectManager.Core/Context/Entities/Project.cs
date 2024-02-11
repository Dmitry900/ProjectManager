using ProjectManager.Core.Context.Entities.Interfaces;

namespace ProjectManager.Core.Context.Entities
{
    public class Project : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CustomerCompany { get; set; }
        public string PerformingCompany { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public Employee Director { get; set; }
        public List<Employee> Employees { get; set; }
    }
}