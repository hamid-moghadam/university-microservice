using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Modules.EF.Infrastructure;
using Curriculum.API.Data.Models;
using Curriculum.API.Dto;
using Curriculum.API.Persistence;
using Curriculum.API.Services.Interfaces;
using Kasp.Data;
using Kasp.Data.EF.Extensions;
using Kasp.Data.Extensions;
using Kasp.Data.Models;
using Kasp.ObjectMapper.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.API.Services
{
    public class StudentCurriculumService :
        AppFilteredServiceBase<CurriculumDbContext, StudentCurriculum, StudentCurriculumFilterDto>,
        IStudentCurriculumService
    {
        public StudentCurriculumService(CurriculumDbContext db) : base(db)
        {
        }

        public override async Task<IPagedList<TOutput>> FilterAsync<TOutput>(StudentCurriculumFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            var query = BaseQuery.AsNoTracking().Where(x => x.Student.UserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.Q))
                query = query.Where(x =>
                    EF.Functions.ILike(x.Curriculum.Course.Title, $"%{filter.Q}%") ||
                    EF.Functions.ILike(x.Curriculum.Teacher.FullName, $"%{filter.Q}%") ||
                    EF.Functions.ILike(x.Curriculum.Field.Title, $"%{filter.Q}%"));

            if (filter.Status.HasValue)
                query = query.Where(x => x.Status == (StudentCurriculumStatus) filter.Status.Value);

            if (filter.SemesterId.HasValue)
                query = query.Where(x => x.Curriculum.Semester.Id == filter.SemesterId.Value);

            return await query.MapTo<TOutput>().SortBy(filter)
                .ToPagedListAsync(filter.Count, filter.Page, cancellationToken);
        }

        public async Task<int> GetCurriculumCount(int semesterId, string userId)
        {
            return await BaseQuery.CountAsync(x =>
                x.Curriculum.Semester.Id == semesterId && x.Student.UserId == userId &&
                x.Status == StudentCurriculumStatus.Accepted);
        }
    }
}