using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Curriculum;
using Grpc.Net.Client;
using HttpAggregator.Dto.Grpc;
using HttpAggregator.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HttpAggregator.Services
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

        public async Task<StudentInformationGrpcDto> GetInfoAsync(string userId)
        {
            _logger.LogInformation("get student information of {0}", userId);
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var coreChannel = GrpcChannel.ForAddress(_configuration["CoreGrpc"]);

            var coreClient = new StudentService.StudentServiceClient(coreChannel);

            var studentInfo = await coreClient.GetInfoAsync(new StudentInfoRequest
            {
                UserId = userId
            });

            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);

            return new StudentInformationGrpcDto
            {
                Code = studentInfo.Code,
                Id = studentInfo.Id,
                CreateTime = DateTime.Parse(studentInfo.CreateTime),
                FieldTitle = studentInfo.FieldTitle,
                FullName = studentInfo.FullName,
                UserId = studentInfo.UserId,
                FieldId = studentInfo.FieldId,
                CurrentSemesterId = studentInfo.CurrentSemesterId,
                CurrentSemesterTitle = studentInfo.CurrentSemesterTitle,
                CanTakeCurriculums = studentInfo.CanTakeCurriculums,
                EndCurriculumScheduleDateTime = studentInfo.CanTakeCurriculums
                    ? DateTime.Parse(studentInfo.EndCurriculumScheduleDateTime)
                    : (DateTime?) null,
                StartCurriculumScheduleDateTime = studentInfo.CanTakeCurriculums
                    ? DateTime.Parse(studentInfo.StartCurriculumScheduleDateTime)
                    : (DateTime?) null
            };
        }
    }
}