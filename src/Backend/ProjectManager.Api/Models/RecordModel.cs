using ProjectManager.Domain.Entities;

namespace ProjectManager.Api.Models
{
    public record RecordModel(Guid Id, RecordType Type, string Text);
}
