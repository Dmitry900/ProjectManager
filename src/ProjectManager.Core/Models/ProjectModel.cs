namespace ProjectManager.Core.Models
{
    public record ProjectModel(Guid Id, string Title, string CustomerCompany, string PerformingCompany, DateTime StartDate, DateTime EndDate, int Priority);
}