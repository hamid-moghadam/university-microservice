using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Curriculum;
using Grpc.Net.Client;
using HttpAggregator.Dto;
using HttpAggregator.Services.Interfaces;
using Kasp.ObjectMapper.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace HttpAggregator.Queries
{
    public class GetStudentInformationQuery : IRequest<StudentInformationDto>
    {
        public GetStudentInformationQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }


        public class Handler : IRequestHandler<GetStudentInformationQuery, StudentInformationDto>
        {
            private readonly IConfiguration _configuration;

            private readonly ICurriculumGrpcService _curriculumGrpcService;
            private readonly IStudentCurriculumGrpcService _studentCurriculumGrpcService;

            public Handler(IConfiguration configuration, ICurriculumGrpcService curriculumGrpcService,
                IStudentCurriculumGrpcService studentCurriculumGrpcService)
            {
                _configuration = configuration;
                _curriculumGrpcService = curriculumGrpcService;
                _studentCurriculumGrpcService = studentCurriculumGrpcService;
            }

            public async Task<StudentInformationDto> Handle(GetStudentInformationQuery request,
                CancellationToken cancellationToken)
            {
                var studentInfo = await _studentCurriculumGrpcService.GetInfoAsync(request.UserId);
                var curriculumInfo =
                    await _curriculumGrpcService.GetOverviewAsync(studentInfo.CurrentSemesterId, request.UserId);

                return new StudentInformationDto
                {
                    Student = studentInfo.MapTo<StudentDto>(),
                    CurriculumOverview = curriculumInfo.MapTo<CurriculumOverviewDto>()
                };
            }
        }
    }
}