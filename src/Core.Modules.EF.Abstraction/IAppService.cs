using System;
using Kasp.Data;
using Kasp.Data.Models.Helpers;

namespace Core.Modules.EF.Abstraction {
	public interface IAppService<TModel, TKey> : IBaseRepository<TModel, TKey> where TKey : IEquatable<TKey> where TModel : class, IModel<TKey> {
	}

	public interface IAppService<TModel> : IAppService<TModel, int> where TModel : class, IModel {
	}
}
