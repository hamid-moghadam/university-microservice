using System;
using Core.API.Attributes;
using Kasp.Data;
using Kasp.Data.Models;
using Kasp.Data.Models.Helpers;
using Kasp.FormBuilder.Services;
using Kasp.ObjectMapper;
using Kasp.Panel.EntityManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers.Panel
{
    [ApiExplorerSettings(GroupName = "panel")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiPanelRoute("[controller]")]
    public abstract class PanelApiController<TModel, TKey, TRepository, TVm, TPartialVm, TEditDto, TFilterDto>
        : EntityManagerControllerBase<TModel, TKey, TRepository, TVm, TPartialVm, TEditDto, TFilterDto>
        where TModel : class, IModel<TKey>
        where TRepository : IFilteredRepositoryBase<TModel, TKey, TFilterDto>
        where TVm : IModel<TKey>, IModel
        where TPartialVm : class, IModel<TKey>
        where TFilterDto : FilterBase
        where TKey : IEquatable<TKey>
        where TEditDto : class
    {
        public PanelApiController(TRepository repository, IObjectMapper objectMapper, IFormBuilder builder) : base(
            repository, objectMapper, builder)
        {
        }
    }

    public abstract class
        PanelApiController<TModel, TRepository, TVm, TPartialVm, TEditDto, TFilterDto> : PanelApiController<TModel, int,
            TRepository, TVm, TPartialVm, TEditDto, TFilterDto>
        where TModel : class, IModel
        where TRepository : IFilteredRepositoryBase<TModel, int, TFilterDto>
        where TVm : IModel
        where TPartialVm : class, IModel
        where TEditDto : class
        where TFilterDto : FilterBase
    {
        protected PanelApiController(TRepository repository, IObjectMapper objectMapper, IFormBuilder builder) : base(
            repository, objectMapper, builder)
        {
        }
    }
}