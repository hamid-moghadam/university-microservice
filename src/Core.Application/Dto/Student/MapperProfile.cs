using Core.Application.Dto.Common;
using Core.Application.Dto.Course;
using Core.Application.Dto.User;
using Core.Events;

namespace Core.Application.Dto.Student
{
    public class MapperProfile : BaseModelMapper<Domain.Student, StudentPartialDto, StudentEditDto>
    {
        public MapperProfile()
        {
            CreateMap<Domain.Student, StudentDto>();
            CreateMap<StudentDto, StudentResponse>();
        }
    }
}