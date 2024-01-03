using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Models
{
    public record RecordModel(Guid Id, RecordType Type, string Text);
}
