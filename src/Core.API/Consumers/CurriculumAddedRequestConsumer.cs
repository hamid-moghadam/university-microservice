using System;
using System.Threading.Tasks;
using Core.Application.Dto.Curriculum;
using Core.Application.Dto.Student;
using Core.Application.Services;
using Core.Domain;
using Core.Events;
using HttpAggregator.Events;
using Kasp.ObjectMapper.Extensions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Core.API.Consumers
{
    public class CurriculumAddedRequestConsumer : IConsumer<ICurriculumAddedRequest>
    {
        private readonly ILogger<CurriculumAddedRequestConsumer> _logger;
        private readonly ICurriculumService _curriculumService;
        private readonly ICurriculumScheduleService _curriculumScheduleService;
        private readonly ISemesterService _semesterService;
        private readonly IStudentService _studentService;

        public CurriculumAddedRequestConsumer(ILogger<CurriculumAddedRequestConsumer> logger,
            ICurriculumService curriculumService, IStudentService studentService,
            ICurriculumScheduleService curriculumScheduleService, ISemesterService semesterService)
        {
            _logger = logger;
            _curriculumService = curriculumService;
            _studentService = studentService;
            _curriculumScheduleService = curriculumScheduleService;
            _semesterService = semesterService;
        }

        public async Task Consume(ConsumeContext<ICurriculumAddedRequest> context)
        {
            _logger.LogInformation("process adding curriculum {0} to user {1}", context.Message.CurriculumId,
                context.Message.UserId);

            var curriculum =
                await _curriculumService.GetAsync<CurriculumDto>(context.Message.CurriculumId);
            var student = await _studentService.GetAsync<StudentDto>(context.Message.StudentId);

            if (curriculum == null || student == null)
            {
                _logger.LogInformation("student or curriculum was not found");
                return;
            }

            var currentSemester = await _semesterService.GetAsync(x => x.ActivatedAt != null);
            var (curriculumSchedule, canTakeCurriculums) = await _curriculumScheduleService.GetCurrentScheduleAsync(
                currentSemester.Id, student.Semester.IntegerTitle,
                student.Field.FieldGroupId);

            if (!canTakeCurriculums)
            {
                _logger.LogInformation("it's not time of adding curriculums");
                return;
            }

            if (currentSemester.Title != curriculum.Semester.Title)
            {
                _logger.LogInformation("You're not allowed to add curriculums from previous semesters");
                return;
            }


            if (curriculum.IsCapacityCompleted)
            {
                _logger.LogInformation("curriculum {0} is completed. failed to add to user {1}",
                    context.Message.CurriculumId,
                    context.Message.UserId);
                await context.Publish<ICurriculumAddedResponse>(new CurriculumAddedResponse
                {
                    CurriculumResponse = curriculum.MapTo<CurriculumResponse>(),
                    StudentResponse = student.MapTo<StudentResponse>(),
                    Status = StudentCurriculumStatus.Rejected,
                    StatusDescription = "Capacity is already completed"
                });
                return;
            }

            var isCompleted = await _curriculumService.ReserveAsync(curriculum.Id);
            //todo: it may increase reversed variable twice if UpdateCurriculum run after CurriculumAdded
            curriculum.ReservedCapacity++;
            if (isCompleted)
                curriculum.IsCapacityCompleted = true;
            await context.Publish<ICurriculumAddedResponse>(new CurriculumAddedResponse
            {
                CurriculumResponse = curriculum.MapTo<CurriculumResponse>(),
                StudentResponse = student.MapTo<StudentResponse>(),
                Status = StudentCurriculumStatus.Accepted,
                StatusDescription = "success"
            });
            _logger.LogInformation("add curriculum process completed");
        }
    }
}