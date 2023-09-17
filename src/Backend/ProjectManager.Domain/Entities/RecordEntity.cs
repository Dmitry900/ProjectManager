namespace ProjectManager.Domain.Entities
{
    public class RecordEntity
    {
        public Guid RecordId { get; set; }
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