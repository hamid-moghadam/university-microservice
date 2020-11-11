using AutoMapper;
using Core.Application.Dto.Student;
using Core.Application.Dto.Teacher;

namespace Core.Application.Dto.User
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDto, StudentPartialDto>();
            CreateMap<UserDto, TeacherPartialDto>();
        }
    }
}