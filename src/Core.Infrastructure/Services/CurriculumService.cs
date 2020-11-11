using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Curriculums;
using Core.Application.Curriculums.Events;
using Core.Application.Services;
using Core.Domain;
using Core.Events;
using Core.Modules.EF.Infrastructure;
using Core.Persistence;
using Kasp.Data;
using Kasp.Data.EF.Extensions;
using Kasp.Data.Extensions;
using Kasp.Data.Models;
using Kasp.ObjectMapper.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Services
{
    public class CurriculumService : AppFilteredServiceBase<CoreDbContext, Curriculum, CurriculumFilterDto>,
        ICurriculumService
    {
        private readonly IMediator _mediator;

        public CurriculumService(CoreDbContext db, IMediator mediator) : base(db)
        {
            _mediator = mediator;
        }

        public override async Task<IPagedList<TOutput>> FilterAsync<TOutput>(CurriculumFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            var query = BaseQuery.AsNoTracking()
                .Where(x => x.SemesterId == filter.SemesterId && x.FieldId == filter.FieldId);

            if (!string.IsNullOrEmpty(filter.Q))
                query = query.Include(x => x.Course).Include(x => x.Field)
                    .Where(x =>
                        EF.Functions.ILike(x.Course.Title, $"%{filter.Q}%") ||
                        EF.Functions.ILike(x.Teacher.FullName, $"%{filter.Q}%") ||
                        EF.Functions.ILike(x.Field.Title, $"%{filter.Q}%"));

            return await query.MapTo<TOutput>().SortBy(filter)
                .ToPagedListAsync(filter.Count, filter.Page, cancellationToken);
        }

        public async Task<bool> ReserveAsync(int id)
        {
            var c = await GetAsync(id);
            c.ReservedCapacity++;
            await UpdateAsync(c);
            if (c.IsCapacityCompleted)
                await _mediator.Publish(new OnCurriculumCompleted(c.Id, c.FieldId));
            await _mediator.Publish(new OnCurriculumUpdated(c.Id));
            return c.IsCapacityCompleted;
        }

        public async Task<int> FreeAsync(int id)
        {
            var c = await GetAsync(id);
            c.ReservedCapacity--;
            await UpdateAsync(c);
            if (c.RemainingCapacity == 1)
                await _mediator.Publish(new OnCurriculumFreed(c.Id, c.RemainingCapacity, c.FieldId));
            await _mediator.Publish(new OnCurriculumUpdated(c.Id));
            return c.Capacity;
        }
    }
}