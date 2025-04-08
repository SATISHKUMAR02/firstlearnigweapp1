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
            CreateMap<Student, Studentdto>().ReverseMap(); // allow bi-directional mapping  , maps student to studentDTO and vise versa
            CreateMap<Student, Student>().ReverseMap(); // maps student to student and viseVersa
            CreateMap<RoleDto, Role>().ReverseMap(); // 
            CreateMap<RolePrivilegeDto, RolePrivilege>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
