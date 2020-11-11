using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class FieldService : AppFilteredServiceBase<CoreDbContext, Field>, IFieldService
    {
        public FieldService(CoreDbContext db) : base(db)
        {
        }

        public override async Task<IPagedList<TOutput>> FilterAsync<TOutput>(FilterBase filter, CancellationToken cancellationToken = default)
        {
            var query = BaseQuery.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.Q))
                query = query.Where(x => x.Title.Contains(filter.Q));

            return await query.MapTo<TOutput>().SortBy(filter).ToPagedListAsync(filter.Count, filter.Page, cancellationToken);
        }
    }
}