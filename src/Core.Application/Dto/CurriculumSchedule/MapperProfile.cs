using Core.Application.Dto.Common;
using Core.Application.Dto.Course;

namespace Core.Application.Dto.CurriculumSchedule
{
    public class MapperProfile : BaseModelMapper<Domain.CurriculumSchedule, CurriculumSchedulePartialDto,
        CurriculumScheduleEditDto>
    {
        public MapperProfile()
        {
            CreateMap<Domain.CurriculumSchedule, CurriculumScheduleDto>();
        }
    }
}