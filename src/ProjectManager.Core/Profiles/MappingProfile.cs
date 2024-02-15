using AutoMapper;
using ProjectManager.Core.Context.Entities;
using ProjectManager.Core.Models;

namespace ProjectManager.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectModel>()
                 .ForSourceMember(it => it.Employees, it => it.DoNotValidate())
                 .ForSourceMember(it => it.Director, it => it.DoNotValidate());
            CreateMap<Employee, EmployeeModel>().ForSourceMember(it => it.Projects, it => it.DoNotValidate());
        }
    }
}