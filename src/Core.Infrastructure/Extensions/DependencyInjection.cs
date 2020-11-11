using Core.Application.Services;
using Core.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICurriculumService, CurriculumService>();
            services.AddScoped<ICurriculumScheduleService, CurriculumScheduleService>();
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<IFieldGroupService, FieldGroupService>();
            services.AddScoped<ISemesterService, SemesterService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IUserGrpcService, UserGrpcGrpcService>();
            return services;
        }
    }
}