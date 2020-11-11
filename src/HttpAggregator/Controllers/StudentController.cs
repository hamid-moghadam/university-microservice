using System.Threading.Tasks;
using HttpAggregator.Attributes;
using HttpAggregator.Dto;
using HttpAggregator.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HttpAggregator.Controllers
{
    [ApiRoute("student")]
    public class StudentController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<StudentInformationDto>> Get()
        {
            var result = await Mediator.Send(new GetStudentInformationQuery(UserId));
            return Ok(result);
        }
    }
}