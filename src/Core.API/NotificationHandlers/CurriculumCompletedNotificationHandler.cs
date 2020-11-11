using System.Threading;
using System.Threading.Tasks;
using Core.Application.Curriculums.Events;
using Core.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.API.NotificationHandlers
{
    public class CurriculumCompletedNotificationHandler : INotificationHandler<OnCurriculumCompleted>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CurriculumCompletedNotificationHandler> _logger;

        public CurriculumCompletedNotificationHandler(IPublishEndpoint publishEndpoint,
            ILogger<CurriculumCompletedNotificationHandler> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(OnCurriculumCompleted notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("curriculum {0} is completed", notification.Id);
            await _publishEndpoint.Publish<ICurriculumCapacityCompleted>(new CurriculumCapacityFreed
            {
                Id = notification.Id,
                FieldId = notification.FieldId
            }, cancellationToken);
        }
    }
}