namespace ProjectManager.Domain.Models
{
    public record TaskModel(Guid Id, Guid BoardId, string Name);
}
