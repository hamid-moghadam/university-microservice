using System.Threading.Tasks;
using Core.Application.Curriculums.Events;
using Core.Application.Dto.Course;
using Core.Application.Dto.Course.Events;
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
    [EntityManagerInfo("درس ها", Name = "courses")]
    public class CourseController : PanelApiController<Course, ICourseService, CoursePartialDto, CoursePartialDto,
        CourseEditDto, FilterBase>
    {
        private readonly IMediator _mediator;

        public CourseController(ICourseService repository, IObjectMapper objectMapper, IFormBuilder builder,
            IMediator mediator) : base(
            repository, objectMapper, builder)
        {
            _mediator = mediator;
        }

        public override async Task<ActionResult<CoursePartialDto>> Update(int id, CourseEditDto model)
        {
            var result = await base.Update(id, model);

            await _mediator.Publish(new OnCourseUpdated(id));
            return result;
        }
    }
}