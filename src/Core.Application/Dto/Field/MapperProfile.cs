using Core.Application.Dto.Common;
using Core.Application.Dto.Course;

namespace Core.Application.Dto.Field
{
    public class MapperProfile : BaseModelMapper<Domain.Field, FieldPartialDto, FieldEditDto>
    {
        public MapperProfile()
        {
            CreateMap<FieldPartialDto, Core.Events.Field>();
            CreateMap<Domain.Field, FieldDto>();
        }
    }
}