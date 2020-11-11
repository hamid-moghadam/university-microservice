using AutoMapper;
using HttpAggregator.Dto.Grpc;

namespace HttpAggregator.Dto
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CurriculumOverviewGrpcDto, CurriculumOverviewDto>();
            CreateMap<StudentInformationGrpcDto, StudentDto>();
        }
    }
}