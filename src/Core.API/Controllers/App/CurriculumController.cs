using System.Threading.Tasks;
using Core.API.Attributes;
using Core.Application.Curriculums;
using Core.Application.Curriculums.Queries;
using Core.Application.Dto.Curriculum;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers.App
{
    [ApiAppRoute("curriculums")]
    public class CurriculumController : BaseAppApiController
    {
        private readonly IStudentService _studentService;
        private readonly ICurriculumService _curriculumService;

        public CurriculumController(IStudentService studentService, ICurriculumService curriculumService)
        {
            _studentService = studentService;
            _curriculumService = curriculumService;
        }

        [HttpGet]
        public async Task<ActionResult<Kasp.Data.PagedResult<CurriculumDto>>> Get([FromQuery] CurriculumFilterDto dto)
        {
            var result = await Mediator.Send(new GetCurriculumsQuery(UserId, dto));
            return Ok(result);
        }
    }
}