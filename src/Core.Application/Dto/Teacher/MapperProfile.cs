using Core.Application.Dto.Common;

namespace Core.Application.Dto.Teacher
{
    public class MapperProfile : BaseModelMapper<Domain.Teacher, TeacherPartialDto, TeacherEditDto>
    {
        public MapperProfile()
        {
            CreateMap<TeacherDto, Domain.Teacher>().ReverseMap();
            CreateMap<TeacherPartialDto, Events.Teacher>();
        }
    }
}