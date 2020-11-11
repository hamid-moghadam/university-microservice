using System.Threading.Tasks;
using Core.Application.Services;
using Curriculum.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Core.API.Consumers
{
    public class CurriculumRemovedResponseConsumer : IConsumer<ICurriculumRemovedResponse>
    {
        private readonly ICurriculumService _curriculumService;
        private readonly ILogger<CurriculumRemovedResponseConsumer> _logger;

        public CurriculumRemovedResponseConsumer(ICurriculumService curriculumService,
            ILogger<CurriculumRemovedResponseConsumer> logger)
        {
            _curriculumService = curriculumService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICurriculumRemovedResponse> context)
        {
            _logger.LogInformation("free capacity of curriculum {0}", context.Message.CurriculumId);
            await _curriculumService.FreeAsync(context.Message.CurriculumId);
            _logger.LogInformation("remove curriculum process completed");
        }
    }
}