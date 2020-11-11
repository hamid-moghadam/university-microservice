using System.Threading.Tasks;
using Core.Events;
using Curriculum.API.Data.Models;
using Curriculum.API.Hubs;
using Curriculum.API.Services.Interfaces;
using Kasp.ObjectMapper.Extensions;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using StudentCurriculumStatus = Curriculum.API.Data.Models.StudentCurriculumStatus;

namespace Curriculum.API.Consumers
{
    public class CurriculumAddedResponseConsumer : IConsumer<ICurriculumAddedResponse>
    {
        private readonly ILogger<CurriculumAddedResponseConsumer> _logger;
        private readonly IStudentCurriculumService _studentCurriculumService;
        private readonly IHubContext<EventsHub, IEventsHub> _hubContext;

        public CurriculumAddedResponseConsumer(ILogger<CurriculumAddedResponseConsumer> logger,
            IStudentCurriculumService studentCurriculumService, IHubContext<EventsHub, IEventsHub> hubContext)
        {
            _logger = logger;
            _studentCurriculumService = studentCurriculumService;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<ICurriculumAddedResponse> context)
        {
            _logger.LogInformation("Adding curriculum {0} to student {1}", context.Message.CurriculumResponse.Id,
                context.Message.StudentResponse.UserId);
            var isAlreadyExists = await _studentCurriculumService.GetAsync(x =>
                x.Student.UserId == context.Message.StudentResponse.UserId &&
                x.Curriculum.Id == context.Message.CurriculumResponse.Id &&
                x.Status == StudentCurriculumStatus.Accepted);
            if (isAlreadyExists != null)
            {
                _logger.LogInformation("user {0} is already added curriculum {1}",
                    context.Message.StudentResponse.UserId, context.Message.CurriculumResponse.Id);
                await _hubContext.Clients.User(context.Message.StudentResponse.UserId)
                    .PrivateMessage($"You've already added curriculum {context.Message.CurriculumResponse.Id}");
                return;
            }

            var studentCurriculum = new StudentCurriculum
            {
                Curriculum = context.Message.CurriculumResponse.MapTo<Data.Models.Curriculum>(),
                Status = (Data.Models.StudentCurriculumStatus) context.Message.Status,
                Student = context.Message.StudentResponse.MapTo<Student>(),
                StatusDescription = context.Message.StatusDescription
            };
            await _studentCurriculumService.AddAsync(studentCurriculum);
            await _hubContext.Clients.User(context.Message.StudentResponse.UserId)
                .StudentCurriculumAdded(studentCurriculum.Id, studentCurriculum.Status,
                    studentCurriculum.StatusDescription);
            await _hubContext.Clients.User(context.Message.StudentResponse.UserId)
                .PrivateMessage($"You successfully added curriculum {context.Message.CurriculumResponse.Id}");
            _logger.LogInformation("user {0} is successfully added curriculum {1}",
                context.Message.StudentResponse.UserId, context.Message.CurriculumResponse.Id);
        }
    }
}