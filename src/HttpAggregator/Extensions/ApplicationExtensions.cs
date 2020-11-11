using HttpAggregator.Services;
using HttpAggregator.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpAggregator.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICurriculumGrpcService, CurriculumGrpcService>();
            services.AddScoped<IStudentCurriculumGrpcService, StudentCurriculumGrpcService>();
            return services;
        }
    }
}