using System.Threading.Tasks;
using Core.Application.Curriculums.Events;
using Core.Application.Dto.Course;
using Core.Application.Dto.Field;
using Core.Application.Dto.Field.Events;
using Core.Application.Services;
using Core.Domain;
using Kasp.Data.Models;
using Kasp.FormBuilder.Services;
using Kasp.ObjectMapper;
using Kasp.Panel.EntityManager;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers.Panel
{
    [EntityManagerInfo("رشته ها", Name = "fields")]
    public class FieldController : PanelApiController<Field, IFieldService,
        FieldPartialDto,
        FieldPartialDto,
        FieldEditDto, FilterBase>
    {
        private readonly IMediator _mediator;

        public FieldController(IFieldService repository, IObjectMapper objectMapper, IFormBuilder builder,
            IMediator mediator) : base(
            repository, objectMapper, builder)
        {
            _mediator = mediator;
        }


        public override async Task<ActionResult<FieldPartialDto>> Update(int id, FieldEditDto model)
        {
            var result = await base.Update(id, model);
            await _mediator.Publish(new OnFieldUpdated(id));
            return result;
        }
    }
}