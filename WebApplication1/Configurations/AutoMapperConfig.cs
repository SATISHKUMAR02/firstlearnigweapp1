using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Students;

namespace WebApplication1.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Student, Studentdto>().ReverseMap(); // allow bi-directional mapping 
            CreateMap<Student, Student>().ReverseMap();
            CreateMap<RoleDto, Role>().ReverseMap();
            CreateMap<RolePrivilegeDto, RolePrivilege>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}