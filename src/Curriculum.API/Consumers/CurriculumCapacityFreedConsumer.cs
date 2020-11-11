using System.Threading.Tasks;
using Core.Events;
using Curriculum.API.Hubs;
using Curriculum.API.Hubs.Groups;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Curriculum.API.Consumers
{
    public class CurriculumCapacityFreedConsumer : IConsumer<ICurriculumCapacityFreed>
    {
        private readonly IHubContext<EventsHub, IEventsHub> _hubContext;
        private readonly ILogger<CurriculumCapacityFreedConsumer> _logger;

        public CurriculumCapacityFreedConsumer(IHubContext<EventsHub, IEventsHub> hubContext,
            ILogger<CurriculumCapacityFreedConsumer> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICurriculumCapacityFreed> context)
        {
            _logger.LogInformation("curriculum {0} is freed. sending this message to socket", context.Message.Id);
            await _hubContext.Clients.Group(EventsHub.GetGroupName(GroupType.Field, context.Message.FieldId))
                .CurriculumFreed(context.Message.Id, context.Message.CurrentCapacity);
        }
    }
}