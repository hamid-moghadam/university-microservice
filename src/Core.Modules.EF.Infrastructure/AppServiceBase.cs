using System;
using Core.Modules.EF.Abstraction;
using Kasp.Data.EF.Data;
using Kasp.Data.Models.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Core.Modules.EF.Infrastructure
{
    public class AppServiceBase<TDbContext, TModel, TKey> : EFBaseRepository<TDbContext, TModel, TKey>,
        IAppService<TModel, TKey>
        where TDbContext : DbContext
        where TModel : class, IModel<TKey>
        where TKey : IEquatable<TKey>
    {
        public AppServiceBase(TDbContext db) : base(db)
        {
        }
    }

    public class AppServiceBase<TDbContext, TModel> : AppServiceBase<TDbContext, TModel, int>
        where TDbContext : DbContext where TModel : class, IModel
    {
        public AppServiceBase(TDbContext db) : base(db)
        {
        }
    }
}