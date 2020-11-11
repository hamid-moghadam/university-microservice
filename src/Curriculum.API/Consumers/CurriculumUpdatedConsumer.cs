using System.Linq;
using System.Threading.Tasks;
using Core.Events;
using Curriculum.API.Hubs;
using Curriculum.API.Hubs.Groups;
using Curriculum.API.Services.Interfaces;
using Kasp.ObjectMapper.Extensions;
using Microsoft.Extensions.Logging;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Curriculum.API.Consumers
{
    public class CurriculumUpdatedConsumer : IConsumer<ICurriculumUpdated>
    {
        private readonly IStudentCurriculumService _studentCurriculumService;
        private readonly IHubContext<EventsHub, IEventsHub> _hubContext;
        private readonly ILogger<CurriculumUpdatedConsumer> _logger;

        public CurriculumUpdatedConsumer(IStudentCurriculumService studentCurriculumService,
            IHubContext<EventsHub, IEventsHub> hubContext, ILogger<CurriculumUpdatedConsumer> logger)
        {
            _studentCurriculumService = studentCurriculumService;
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICurriculumUpdated> context)
        {
            var curriculum = context.Message.Curriculum;
            var curriculumDto = curriculum.MapTo<Data.Models.Curriculum>();
            _logger.LogInformation("Update curriculum {0}", JsonSerializer.Serialize(curriculum));
            _logger.LogInformation("Update curriculumDto {0}", JsonSerializer.Serialize(curriculumDto));
            var studentCurriculums =
                (await _studentCurriculumService.ListAsync(x => x.Curriculum.Id == curriculum.Id)).ToList();
            _logger.LogInformation("update {0} student curriculums with curriculum id of {1}",
                studentCurriculums.Count(), curriculum.Id);
            for (var i = 0; i < studentCurriculums.Count; i++)
            {
                studentCurriculums[i].Curriculum = curriculumDto;
                await _studentCurriculumService.UpdateAsync(studentCurriculums[i]);
            }

            await _hubContext.Clients.Group(EventsHub.GetGroupName(GroupType.Field, curriculum.Field.Id))
                .CurriculumUpdated(curriculum.Id, curriculumDto);
        }
    }
}