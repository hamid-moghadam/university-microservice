using Core.Application.Dto.CurriculumSchedule;
using Core.Application.Services;
using Core.Domain;
using Kasp.Data.Models;
using Kasp.FormBuilder.Services;
using Kasp.ObjectMapper;
using Kasp.Panel.EntityManager;

namespace Core.API.Controllers.Panel
{
    [EntityManagerInfo("زمان‌بندی ارائه ها", Name = "curriculum-schedules")]
    public class CurriculumScheduleController : PanelApiController<CurriculumSchedule, ICurriculumScheduleService,
        CurriculumSchedulePartialDto,
        CurriculumSchedulePartialDto,
        CurriculumScheduleEditDto, FilterBase>
    {
        public CurriculumScheduleController(ICurriculumScheduleService repository, IObjectMapper objectMapper,
            IFormBuilder builder) : base(repository, objectMapper, builder)
        {
        }
    }
}