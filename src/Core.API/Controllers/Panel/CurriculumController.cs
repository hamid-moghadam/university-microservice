using System.Threading.Tasks;
using Core.Application.Curriculums;
using Core.Application.Curriculums.Events;
using Core.Application.Dto.Curriculum;
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
    [EntityManagerInfo("ارائه ها", Name = "curriculums")]
    public class CurriculumController : PanelApiController<Domain.Curriculum, ICurriculumService, CurriculumPartialDto,
        CurriculumPartialDto,
        CurriculumEditDto, CurriculumFilterDto>
    {
        private readonly IMediator _mediator;

        public CurriculumController(ICurriculumService repository, IObjectMapper objectMapper, IFormBuilder builder,
            IMediator mediator) :
            base(repository, objectMapper, builder)
        {
            _mediator = mediator;
        }

        public override async Task<ActionResult<CurriculumPartialDto>> Update(int id, CurriculumEditDto model)
        {
            var result = await base.Update(id, model);
            await _mediator.Publish(new OnCurriculumUpdated(id));
            return result;
        }
    }
}