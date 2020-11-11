using Core.Application.Dto.FieldGroup;
using Core.Application.Services;
using Core.Domain;
using Kasp.Data.Models;
using Kasp.FormBuilder.Services;
using Kasp.ObjectMapper;
using Kasp.Panel.EntityManager;

namespace Core.API.Controllers.Panel
{
    [EntityManagerInfo("گروه آموزشی", Name = "field-groups")]
    public class FieldGroupController : PanelApiController<FieldGroup, IFieldGroupService,
        FieldGroupPartialDto,
        FieldGroupPartialDto,
        FieldGroupEditDto, FilterBase>
    {
        public FieldGroupController(IFieldGroupService repository, IObjectMapper objectMapper, IFormBuilder builder) :
            base(repository, objectMapper, builder)
        {
        }
    }
}