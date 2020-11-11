using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Curriculums.Events;
using Core.Application.Dto.Field.Events;
using Core.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.API.NotificationHandlers
{
    public class FieldUpdatedNotificationHandler : INotificationHandler<OnFieldUpdated>
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IMediator _mediator;
        private readonly ILogger<FieldUpdatedNotificationHandler> _logger;

        public FieldUpdatedNotificationHandler(ICurriculumService curriculumService, IMediator mediator,
            ILogger<FieldUpdatedNotificationHandler> logger)
        {
            _curriculumService = curriculumService;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(OnFieldUpdated notification, CancellationToken cancellationToken)
        {
            var curriculumsId =
                (await _curriculumService.ListAsync(x => x.FieldId == notification.Id, cancellationToken)).Select(x =>
                    x.Id);

            _logger.LogInformation("field {0} updated. updating {1} curriculums", notification.Id,
                curriculumsId.Count());

            foreach (var id in curriculumsId)
            {
                await _mediator.Publish(new OnCurriculumUpdated(id), cancellationToken);
            }
        }
    }
}