using Core.Application.Dto.Common;
using Core.Application.Dto.Course;

namespace Core.Application.Dto.Semester
{
    public class MapperProfile : BaseModelMapper<Domain.Semester, SemesterPartialDto, SemesterEditDto>
    {
        public MapperProfile()
        {
            CreateMap<SemesterPartialDto, Events.Semester>();
        }
    }
}