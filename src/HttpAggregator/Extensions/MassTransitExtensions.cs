using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpAggregator.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                    configurator.Host(configuration["Rabbitmq_Host"], cfg =>
                    {
                        cfg.Username(configuration["Rabbitmq_Username"]);
                        cfg.Password(configuration["Rabbitmq_Password"]);
                    }));
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}