using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Dto.Curriculum;
using Core.Application.Helpers;
using Core.Application.Services;
using Kasp.Data;
using Kasp.HttpException.Core;
using MediatR;

namespace Core.Application.Curriculums.Queries
{
    public class GetCurriculumsQuery : IAuthenticatedRequest<PagedResult<CurriculumDto>>
    {
        public GetCurriculumsQuery(string userId, CurriculumFilterDto filterDto)
        {
            UserId = userId;
            FilterDto = filterDto;
        }

        public string UserId { get; }
        public CurriculumFilterDto FilterDto { get; }


        public class Handler : IRequestHandler<GetCurriculumsQuery, PagedResult<CurriculumDto>>
        {
            private readonly ICurriculumService _curriculumService;
            private readonly IStudentService _studentService;

            public Handler(ICurriculumService curriculumService, IStudentService studentService)
            {
                _curriculumService = curriculumService;
                _studentService = studentService;
            }

            public async Task<PagedResult<CurriculumDto>> Handle(GetCurriculumsQuery request,
                CancellationToken cancellationToken)
            {
                var student = await _studentService.GetAsync(x => x.UserId == request.UserId, cancellationToken);
                if (student == null)
                    throw new BadRequestException("You're not student");
                request.FilterDto.FieldId = student.FieldId;

                return (await _curriculumService.FilterAsync<CurriculumDto>(request.FilterDto, cancellationToken))
                    .ToPagedResult();
            }
        }
    }
}