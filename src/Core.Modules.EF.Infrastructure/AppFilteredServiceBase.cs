using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Modules.EF.Abstraction;
using Kasp.Data;
using Kasp.Data.Extensions;
using Kasp.Data.Models;
using Kasp.Data.Models.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Core.Modules.EF.Infrastructure {
	public abstract class AppFilteredServiceBase<TDbContext, TModel, TKey, TFilter> : AppServiceBase<TDbContext, TModel, TKey>, IAppFilteredService<TModel, TKey, TFilter>
		where TDbContext : DbContext
		where TModel : class, IModel<TKey>
		where TKey : IEquatable<TKey>
		where TFilter : FilterBase {
		public AppFilteredServiceBase(TDbContext db) : base(db) {
		}

		public abstract Task<IPagedList<TOutput>> FilterAsync<TOutput>(TFilter filter, CancellationToken cancellationToken = default);

		public async ValueTask<TProject> GetAsync<TProject>(TKey id, Expression<Func<TModel, TProject>> projection, CancellationToken cancellationToken = default) where TProject : IModel<TKey> {
			return await BaseQuery.Select(projection).WhereIdEquals(x => x.Id, id).FirstOrDefaultAsync(cancellationToken);
		}

		public async ValueTask<TProject> GetAsync<TProject>(Expression<Func<TModel, bool>> filter, Expression<Func<TModel, TProject>> projection, CancellationToken cancellationToken = default)
			where TProject : IModel<TKey> {
			return await BaseQuery.Where(filter).Select(projection).FirstOrDefaultAsync(cancellationToken);
		}
	}

	public abstract class AppFilteredServiceBase<TDbContext, TModel, TFilter> : AppFilteredServiceBase<TDbContext, TModel, int, TFilter>
		where TDbContext : DbContext
		where TModel : class, IModel
		where TFilter : FilterBase {
		public AppFilteredServiceBase(TDbContext db) : base(db) {
		}
	}

	public abstract class AppFilteredServiceBase<TDbContext, TModel> : AppFilteredServiceBase<TDbContext, TModel, int, FilterBase>
		where TDbContext : DbContext
		where TModel : class, IModel {
		public AppFilteredServiceBase(TDbContext db) : base(db) {
		}
	}
}
