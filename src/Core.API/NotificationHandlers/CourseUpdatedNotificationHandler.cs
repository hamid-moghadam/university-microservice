using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Curriculums.Events;
using Core.Application.Dto.Course.Events;
using Core.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.API.NotificationHandlers
{
    public class CourseUpdatedNotificationHandler : INotificationHandler<OnCourseUpdated>
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IMediator _mediator;
        private readonly ILogger<CourseUpdatedNotificationHandler> _logger;

        public CourseUpdatedNotificationHandler(ICurriculumService curriculumService, IMediator mediator,
            ILogger<CourseUpdatedNotificationHandler> logger)
        {
            _curriculumService = curriculumService;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(OnCourseUpdated notification, CancellationToken cancellationToken)
        {
            var curriculumsId =
                (await _curriculumService.ListAsync(x => x.CourseId == notification.Id, cancellationToken)).Select(x =>
                    x.Id);

            _logger.LogInformation("course {0} updated. updating {1} curriculums", notification.Id,
                curriculumsId.Count());

            foreach (var id in curriculumsId)
            {
                await _mediator.Publish(new OnCurriculumUpdated(id), cancellationToken);
            }
        }
    }
}