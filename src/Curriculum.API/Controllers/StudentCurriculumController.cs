using System.Threading.Tasks;
using Curriculum.API.Attributes;
using Curriculum.API.Dto;
using Curriculum.API.Services.Interfaces;
using Kasp.Data;
using Microsoft.AspNetCore.Mvc;
using Curriculum.API.Data.Models;

namespace Curriculum.API.Controllers
{
    [ApiRoute("curriculums")]
    public class StudentCurriculumController : BaseApiController
    {
        private readonly IStudentCurriculumService _studentCurriculumService;

        public StudentCurriculumController(IStudentCurriculumService studentCurriculumService)
        {
            _studentCurriculumService = studentCurriculumService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Data.Models.Curriculum>>> List([FromQuery] StudentCurriculumFilterDto dto)
        {
            dto.UserId = UserId;
            var result = await _studentCurriculumService.FilterAsync<StudentCurriculumDto>(dto);
            return Ok(result.ToPagedResult());
        }
    }
}