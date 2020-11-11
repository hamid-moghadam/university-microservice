using System.Threading.Tasks;
using Curriculum.API.Hubs;
using Curriculum.API.Services.Interfaces;
using Curriculum.Events;
using HttpAggregator.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Curriculum.API.Consumers
{
    public class CurriculumRemovedRequestConsumer : IConsumer<ICurriculumRemovedRequest>
    {
        private readonly ILogger<CurriculumRemovedRequestConsumer> _logger;
        private readonly IStudentCurriculumService _studentCurriculumService;
        private readonly IHubContext<EventsHub, IEventsHub> _hubContext;
        private readonly IStudentCurriculumGrpcService _studentCurriculumGrpcService;


        public CurriculumRemovedRequestConsumer(ILogger<CurriculumRemovedRequestConsumer> logger,
            IStudentCurriculumService studentCurriculumService, IHubContext<EventsHub, IEventsHub> hubContext,
            IStudentCurriculumGrpcService studentCurriculumGrpcService)
        {
            _logger = logger;
            _studentCurriculumService = studentCurriculumService;
            _hubContext = hubContext;
            _studentCurriculumGrpcService = studentCurriculumGrpcService;
        }

        public async Task Consume(ConsumeContext<ICurriculumRemovedRequest> context)
        {
            _logger.LogInformation("process removing curriculum {0} to user {1}", context.Message.CurriculumId,
                context.Message.UserId);
            var sc = await _studentCurriculumService.GetAsync(x =>
                x.Curriculum.Id == context.Message.CurriculumId && x.Student.Id == context.Message.StudentId);

            var semesterStatus =
                await _studentCurriculumGrpcService.GetSemesterStatusAsync(context.Message.CurriculumId,
                    context.Message.StudentId);

            if (!semesterStatus.CanTakeCurriculums)
            {
                await _hubContext.Clients.User(context.Message.UserId)
                    .PrivateMessage("Time of adding/removing curriculums is over.");
                return;
            }

            if (!semesterStatus.IsCurriculumSemesterValid)
            {
                await _hubContext.Clients.User(context.Message.UserId)
                    .PrivateMessage("You can't remove curriculums from previous semesters");
                return;
            }

            if (sc != null)
            {
                int id = sc.Id;
                int curriculumId = sc.Curriculum.Id;
                await _studentCurriculumService.RemoveAsync(sc);
                await _hubContext.Clients.User(sc.Student.UserId).StudentCurriculumRemoved(id);
                await context.Publish<ICurriculumRemovedResponse>(new CurriculumRemovedResponse
                {
                    CurriculumId = curriculumId
                });
                await _hubContext.Clients.User(context.Message.UserId)
                    .PrivateMessage($"You successfully removed curriculum {context.Message.CurriculumId}");
                _logger.LogInformation("user {0} successfully added curriculum {1}", context.Message.CurriculumId);
            }
        }
    }
}