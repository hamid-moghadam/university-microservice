using System.Threading.Tasks;
using Curriculum.API.Services.Interfaces;
using Grpc.Core;
using Grpc.Curriculum;
using Microsoft.Extensions.Logging;

namespace Curriculum.API.Grpc
{
    public class StudentCurriculumGrpcService : StudentCurriculumService.StudentCurriculumServiceBase
    {
        private readonly IStudentCurriculumService _studentCurriculumService;
        private ILogger<StudentCurriculumGrpcService> _logger;

        public StudentCurriculumGrpcService(IStudentCurriculumService studentCurriculumService,
            ILogger<StudentCurriculumGrpcService> logger)
        {
            _studentCurriculumService = studentCurriculumService;
            _logger = logger;
        }

        public override async Task<OverviewReply> GetOverview(OverviewRequest request, ServerCallContext context)
        {
            var result = await _studentCurriculumService.GetCurriculumCount(request.SemesterId, request.UserId);
            _logger.LogInformation("Get curriculum overview of user {0} is {1}", request.UserId, result);
            return new OverviewReply
            {
                TotalCount = result
            };
        }
    }
}