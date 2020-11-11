using Core.Application.Dto.Semester;
using Core.Application.Services;
using Core.Domain;
using Kasp.Data.Models;
using Kasp.FormBuilder.Services;
using Kasp.ObjectMapper;
using Kasp.Panel.EntityManager;

namespace Core.API.Controllers.Panel
{
    [EntityManagerInfo("نیمسال", Name = "semesters")]
    public class SemesterController:PanelApiController<Semester, ISemesterService,
        SemesterPartialDto,
        SemesterPartialDto,
        SemesterEditDto, FilterBase>
    {
        public SemesterController(ISemesterService repository, IObjectMapper objectMapper, IFormBuilder builder) : base(repository, objectMapper, builder)
        {
        }
    }
}