using AutoMapper;
using Kasp.Data.Models.Helpers;

namespace Core.Application.Dto.Common
{
    public abstract class BaseModelMapper<TModel, TPartialDto, TEditDto> : Profile where TModel : IModel
    {
        public BaseModelMapper()
        {
            CreateMap<TModel, TPartialDto>().ReverseMap();
            CreateMap<TModel, TEditDto>().ReverseMap();
            CreateMap<TEditDto, TPartialDto>().ReverseMap();
        }
    }
}