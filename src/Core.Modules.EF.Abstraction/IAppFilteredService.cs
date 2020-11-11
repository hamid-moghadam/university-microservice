using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Kasp.Data;
using Kasp.Data.Models;
using Kasp.Data.Models.Helpers;

namespace Core.Modules.EF.Abstraction {
	public interface IAppFilteredService<TModel, TKey, TFilter> : IFilteredRepositoryBase<TModel, TKey, TFilter>
		where TModel : class, IModel<TKey>
		where TKey : IEquatable<TKey>
		where TFilter : FilterBase {
		ValueTask<TProject> GetAsync<TProject>(TKey id, Expression<Func<TModel, TProject>> projection, CancellationToken cancellationToken = default) where TProject : IModel<TKey>;

		ValueTask<TProject> GetAsync<TProject>(Expression<Func<TModel, bool>> filter, Expression<Func<TModel, TProject>> projection, CancellationToken cancellationToken = default)
			where TProject : IModel<TKey>;
	}

	public interface IAppFilteredService<TModel, TFilter> : IAppFilteredService<TModel, int, TFilter>
		where TFilter : FilterBase
		where TModel : class, IModel {
	}

	public interface IAppFilteredService<TModel> : IAppFilteredService<TModel, FilterBase> where TModel : class, IModel {
	}
}
