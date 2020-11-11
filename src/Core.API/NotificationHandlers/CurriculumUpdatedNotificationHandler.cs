using System.Threading;
using System.Threading.Tasks;
using Core.Application.Curriculums.Events;
using Core.Application.Dto.Curriculum;
using Core.Application.Services;
using Core.Events;
using Kasp.ObjectMapper.Extensions;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.API.NotificationHandlers
{
    public class CurriculumUpdatedNotificationHandler : INotificationHandler<OnCurriculumUpdated>
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CurriculumUpdatedNotificationHandler> _logger;

        public CurriculumUpdatedNotificationHandler(ICurriculumService curriculumService,
            IPublishEndpoint publishEndpoint, ILogger<CurriculumUpdatedNotificationHandler> logger)
        {
            _curriculumService = curriculumService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(OnCurriculumUpdated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("curriculum {0} is being updated", notification.Id);
            var curriculumDto = await _curriculumService.GetAsync<CurriculumDto>(notification.Id, cancellationToken);
            await _publishEndpoint.Publish<ICurriculumUpdated>(new CurriculumUpdated
            {
                Curriculum = curriculumDto.MapTo<CurriculumResponse>()
            }, cancellationToken);
        }
    }
}