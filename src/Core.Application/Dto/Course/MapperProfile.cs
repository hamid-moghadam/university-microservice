using AutoMapper;
using Core.Application.Dto.Common;

namespace Core.Application.Dto.Course
{
    public class MapperProfile : BaseModelMapper<Domain.Course, CoursePartialDto, CourseEditDto>
    {
        public MapperProfile()
        {
            CreateMap<CoursePartialDto, Core.Events.Course>();
        }
    }
}