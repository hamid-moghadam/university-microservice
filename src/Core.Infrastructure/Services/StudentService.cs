﻿using System.Linq;
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
    public class StudentService : AppFilteredServiceBase<CoreDbContext, Student>, IStudentService
    {
        public StudentService(CoreDbContext db) : base(db)
        {
        }

        public override async Task<IPagedList<TOutput>> FilterAsync<TOutput>(FilterBase filter, CancellationToken cancellationToken = default)
        {
            var query = BaseQuery.AsNoTracking();

            return await query.MapTo<TOutput>().SortBy(filter).ToPagedListAsync(filter.Count, filter.Page, cancellationToken);
        }
    }
}