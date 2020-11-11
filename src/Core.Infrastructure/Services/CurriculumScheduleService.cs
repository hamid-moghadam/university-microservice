using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Dto.CurriculumSchedule;
using Core.Application.Services;
using Core.Domain;
using Core.Modules.EF.Infrastructure;
using Core.Persistence;
using Kasp.Data;
using Kasp.Data.EF.Extensions;
using Kasp.Data.Extensions;
using Kasp.Data.Models;
using Kasp.ObjectMapper.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Services
{
    public class CurriculumScheduleService : AppFilteredServiceBase<CoreDbContext, CurriculumSchedule>,
        ICurriculumScheduleService
    {
        public CurriculumScheduleService(CoreDbContext db) : base(db)
        {
        }

        public override async Task<IPagedList<TOutput>> FilterAsync<TOutput>(FilterBase filter,
            CancellationToken cancellationToken = default)
        {
            var query = BaseQuery.AsNoTracking();

            return await query.MapTo<TOutput>().SortBy(filter)
                .ToPagedListAsync(filter.Count, filter.Page, cancellationToken);
        }

        public async Task<Tuple<CurriculumScheduleDto, bool>> GetCurrentScheduleAsync(int currentSemesterId,
            int studentSemesterIntegerTitle, int fieldGroupId)
        {
            var now = DateTime.Now;
            var curriculumSchedule = await GetAsync<CurriculumScheduleDto>(x =>
                x.CurrentSemesterId == currentSemesterId && x.FieldGroupId == fieldGroupId);
            bool canTakeCurriculums = curriculumSchedule != null &&
                                      studentSemesterIntegerTitle >= curriculumSchedule.FromSemester.IntegerTitle &&
                                      studentSemesterIntegerTitle <= curriculumSchedule.ToSemester.IntegerTitle &&
                                      now >= curriculumSchedule.Start && now <= curriculumSchedule.End;

            return new Tuple<CurriculumScheduleDto, bool>(curriculumSchedule, canTakeCurriculums);
        }
    }
}