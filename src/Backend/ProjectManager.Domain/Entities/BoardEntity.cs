namespace ProjectManager.Domain.Entities
{
    public class BoardEntity
    {
        public Guid BoardId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<string> Statuses { get; set; }
    }
}
