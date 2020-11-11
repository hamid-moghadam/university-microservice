using System;
using System.Threading.Tasks;
using Curriculum.API.Dto.Grpc;
using Curriculum.API.Services.Interfaces;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Curriculum.API.Services
{
    public class StudentCurriculumGrpcService : IStudentCurriculumGrpcService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StudentCurriculumGrpcService> _logger;

        public StudentCurriculumGrpcService(IConfiguration configuration, ILogger<StudentCurriculumGrpcService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<SemesterStatusDto> GetSemesterStatusAsync(int curriculumId, int studentId)
        {
            _logger.LogInformation("get semester status of curriculum {0}", curriculumId);
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var coreChannel = GrpcChannel.ForAddress(_configuration["CoreGrpc"]);

            var coreClient = new StudentService.StudentServiceClient(coreChannel);

            var semesterStatus = await coreClient.GetSemesterStatusAsync(new SemesterStatusRequest
            {
                CurriculumId = curriculumId,
                StudentId = studentId
            });

            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);

            return new SemesterStatusDto
            {
                CanTakeCurriculums = semesterStatus.CanTakeCurriculums,
                IsCurriculumSemesterValid = semesterStatus.IsCurriculumSemesterValid
            };
        }
    }
}