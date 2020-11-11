using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Dto.Curriculum;
using Core.Application.Dto.CurriculumSchedule;
using Core.Application.Dto.Student;
using Core.Application.Services;
using Core.Domain;
using Grpc.Core;
using Kasp.ObjectMapper.Extensions;
using Microsoft.Extensions.Logging;

namespace Core.API.Grpc
{
    public class StudentGrpcService : StudentService.StudentServiceBase
    {
        private readonly IUserGrpcService _userGrpcService;
        private readonly IStudentService _studentService;
        private readonly ICurriculumService _curriculumService;
        private readonly ISemesterService _semesterService;
        private readonly ICurriculumScheduleService _curriculumScheduleService;
        private readonly ILogger<StudentGrpcService> _logger;

        public StudentGrpcService(IUserGrpcService userGrpcService, IStudentService studentService,
            ILogger<StudentGrpcService> logger, ISemesterService semesterService,
            ICurriculumScheduleService curriculumSchedule, ICurriculumScheduleService curriculumScheduleService,
            ICurriculumService curriculumService)
        {
            _userGrpcService = userGrpcService;
            _studentService = studentService;
            _logger = logger;
            _semesterService = semesterService;
            _curriculumScheduleService = curriculumScheduleService;
            _curriculumService = curriculumService;
        }

        public override async Task<StudentInfoReply> GetInfo(StudentInfoRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Get student information with {0} id", request.UserId);
            var student =
                await _studentService.GetAsync<StudentDto>(x => x.UserId == request.UserId, new CancellationToken());
            var user = await _userGrpcService.GetAsync(student.UserId);
            var currentSemester = await _semesterService.GetAsync(x => x.ActivatedAt != null);
            var (curriculumSchedule, canTakeCurriculums) = await _curriculumScheduleService.GetCurrentScheduleAsync(
                currentSemester.Id, student.Semester.IntegerTitle,
                student.Field.FieldGroupId);
            student = user.MapTo(student);
            return new StudentInfoReply
            {
                Code = student.Code,
                Id = student.Id,
                FieldId = student.Field.Id,
                CreateTime = student.CreateTime.ToString(CultureInfo.InvariantCulture),
                FieldTitle = student.Field.Title,
                FullName = student.FullName,
                UserId = student.UserId,
                CurrentSemesterId = currentSemester.Id,
                CurrentSemesterTitle = currentSemester.Title,
                CanTakeCurriculums = canTakeCurriculums,
                EndCurriculumScheduleDateTime = canTakeCurriculums
                    ? curriculumSchedule.End.ToString(CultureInfo.InvariantCulture)
                    : "",
                StartCurriculumScheduleDateTime = canTakeCurriculums
                    ? curriculumSchedule.Start.ToString(CultureInfo.InvariantCulture)
                    : ""
            };
        }

        public override async Task<SemesterStatusResponse> GetSemesterStatus(SemesterStatusRequest request,
            ServerCallContext context)
        {
            var currentSemester = await _semesterService.GetAsync(x => x.ActivatedAt != null);
            var student =
                await _studentService.GetAsync<StudentDto>(x => x.Id == request.StudentId, new CancellationToken());
            var (curriculumSchedule, canTakeCurriculums) =
                await _curriculumScheduleService.GetCurrentScheduleAsync(currentSemester.Id,
                    student.Semester.IntegerTitle,
                    student.Field.FieldGroupId);
            var curriculum = await _curriculumService.GetAsync<CurriculumDto>(request.CurriculumId);

            return new SemesterStatusResponse
            {
                CanTakeCurriculums = canTakeCurriculums,
                IsCurriculumSemesterValid = currentSemester.Title == curriculum.Semester.Title
            };
        }
    }
}