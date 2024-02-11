namespace ProjectManager.Core.Services
{
    public enum SearchDate
    {
        Start,
        End
    }

    public enum SearchPriorityOption
    {
        StrictEqual,
        LessOrEqual,
        AboveOrEqual,
        Less,
        Above
    }
    public enum ProjectOrderProperty
    {
        None,
        Title,
        CustomerCompany,
        PerformingCompany,
        StartDate,
        EndDate,
        Priority
    }

    public enum Order
    {
        Ascending,
        Descending
    }
}