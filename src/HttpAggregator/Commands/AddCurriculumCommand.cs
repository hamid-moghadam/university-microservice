using System.Threading;
using System.Threading.Tasks;
using Grpc.Core.Logging;
using HttpAggregator.Events;
using HttpAggregator.Services.Interfaces;
using Kasp.HttpException.Core;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HttpAggregator.Commands
{
    public class AddCurriculumCommand : IRequest<Unit>
    {
        public AddCurriculumCommand(string userId, int curriculumId)
        {
            UserId = userId;
            CurriculumId = curriculumId;
        }

        public string UserId { get; }
        public int CurriculumId { get; }

        public class Handler : IRequestHandler<AddCurriculumCommand, Unit>
        {
            private readonly ILogger<AddCurriculumCommand> _logger;
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IStudentCurriculumGrpcService _studentCurriculumGrpcService;

            public Handler(ILogger<AddCurriculumCommand> logger, IPublishEndpoint publishEndpoint,
                IStudentCurriculumGrpcService studentCurriculumGrpcService)
            {
                _logger = logger;
                _publishEndpoint = publishEndpoint;
                _studentCurriculumGrpcService = studentCurriculumGrpcService;
            }

            public async Task<Unit> Handle(AddCurriculumCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Add curriculum to user {0}", request.UserId);
                var studentInformation = await _studentCurriculumGrpcService.GetInfoAsync(request.UserId);
                if (!studentInformation.CanTakeCurriculums)
                    throw new BadRequestException("Time of adding/removing curriculums is over.");
                // Check validations and call other services
                await _publishEndpoint.Publish<ICurriculumAddedRequest>(new CurriculumAddedRequest
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