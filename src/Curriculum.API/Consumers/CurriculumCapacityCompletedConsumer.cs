using System.Threading.Tasks;
using Core.Events;
using Curriculum.API.Hubs;
using Curriculum.API.Hubs.Groups;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Curriculum.API.Consumers
{
    public class CurriculumCapacityCompletedConsumer : IConsumer<ICurriculumCapacityCompleted>
    {
        private readonly IHubContext<EventsHub, IEventsHub> _hubContext;
        private readonly ILogger<CurriculumCapacityCompletedConsumer> _logger;

        public CurriculumCapacityCompletedConsumer(IHubContext<EventsHub, IEventsHub> hubContext,
            ILogger<CurriculumCapacityCompletedConsumer> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICurriculumCapacityCompleted> context)
        {
            _logger.LogInformation("curriculum capacity of {0} is completed. sending this message to socket", context.Message.CurriculumId);
            await _hubContext.Clients.Group(EventsHub.GetGroupName(GroupType.Field, context.Message.FieldId))
                .CurriculumCompleted(context.Message.CurriculumId);
        }
    }
}