using System.Threading;
using System.Threading.Tasks;
using HttpAggregator.Events;
using HttpAggregator.Services.Interfaces;
using Kasp.HttpException.Core;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HttpAggregator.Commands
{
    public class RemoveCurriculumCommand : IRequest<Unit>
    {
        public RemoveCurriculumCommand(string userId, int curriculumId)
        {
            UserId = userId;
            CurriculumId = curriculumId;
        }

        public string UserId { get; }
        public int CurriculumId { get; }

        public class Handler : IRequestHandler<RemoveCurriculumCommand, Unit>
        {
            private readonly ILogger<RemoveCurriculumCommand> _logger;
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IStudentCurriculumGrpcService _studentCurriculumGrpcService;

            public Handler(ILogger<RemoveCurriculumCommand> logger, IPublishEndpoint publishEndpoint,
                IStudentCurriculumGrpcService studentCurriculumGrpcService)
            {
                _logger = logger;
                _publishEndpoint = publishEndpoint;
                _studentCurriculumGrpcService = studentCurriculumGrpcService;
            }

            public async Task<Unit> Handle(RemoveCurriculumCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Remove curriculum from user {0}", request.UserId);
                var studentInformation = await _studentCurriculumGrpcService.GetInfoAsync(request.UserId);
                if (!studentInformation.CanTakeCurriculums)
                    throw new BadRequestException("Time of adding/removing curriculums is over.");
                // Check validations and call other services
                await _publishEndpoint.Publish<ICurriculumRemovedRequest>(new CurriculumRemovedRequest
                {
                    CurriculumId = request.CurriculumId,
                    UserId = request.UserId,
                    StudentId = studentInformation.Id
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}