using Curriculum.API.Services;
using Curriculum.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curriculum.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentCurriculumService, StudentCurriculumService>();
            services.AddScoped<IStudentCurriculumGrpcService, StudentCurriculumGrpcService>();
            return services;
        }
    }
}