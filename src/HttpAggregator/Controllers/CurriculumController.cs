using System.Threading.Tasks;
using HttpAggregator.Attributes;
using HttpAggregator.Commands;
using HttpAggregator.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HttpAggregator.Controllers
{
    [ApiRoute("curriculums")]
    public class CurriculumController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddCurriculumInputDto dto)
        {
            await Mediator.Send(new AddCurriculumCommand(UserId, dto.CurriculumId));
            return Created("", new object());
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] AddCurriculumInputDto dto)
        {
            await Mediator.Send(new RemoveCurriculumCommand(UserId, dto.CurriculumId));
            return NoContent();
        }
    }
}