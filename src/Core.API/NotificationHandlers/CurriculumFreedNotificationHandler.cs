using System.Threading;
using System.Threading.Tasks;
using Core.Application.Curriculums.Events;
using Core.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.API.NotificationHandlers
{
    public class CurriculumFreedNotificationHandler : INotificationHandler<OnCurriculumFreed>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CurriculumFreedNotificationHandler> _logger;

        public CurriculumFreedNotificationHandler(IPublishEndpoint publishEndpoint,
            ILogger<CurriculumFreedNotificationHandler> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(OnCurriculumFreed notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("curriculum {0} is freed", notification.Id);
            await _publishEndpoint.Publish<ICurriculumCapacityFreed>(new CurriculumCapacityFreed
            {
                Id = notification.Id,
                CurrentCapacity = notification.CurrentCapacity,
                FieldId = notification.FieldId
            }, cancellationToken);
        }
    }
}