using Curriculum.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Curriculum.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<CurriculumDbContext>(builder =>
            {
                builder.UseNpgsql(configuration["ConnectionString"]);
                builder.UseLoggerFactory(LoggerFactory.Create(b => { b.AddConsole(); }));
            });

            return services;
        }
    }
}