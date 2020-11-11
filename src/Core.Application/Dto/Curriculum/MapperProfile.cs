using Core.Application.Dto.Common;
using Core.Application.Dto.Course;
using Core.Events;

namespace Core.Application.Dto.Curriculum
{
    public class MapperProfile : BaseModelMapper<Domain.Curriculum, CurriculumPartialDto, CurriculumEditDto>
    {
        public MapperProfile()
        {
            CreateMap<Domain.Curriculum, CurriculumDto>();
            CreateMap<CurriculumDto, CurriculumResponse>().ReverseMap();
        }
    }
}