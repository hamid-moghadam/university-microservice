using Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<CoreDbContext>(builder =>
            {
                builder.UseNpgsql(configuration["ConnectionString"]);
                builder.UseLoggerFactory(LoggerFactory.Create(b => { b.AddConsole(); }));
            });

            return services;
        }
    }
}