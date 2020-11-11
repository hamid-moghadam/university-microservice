using Core.API.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.API.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CurriculumAddedRequestConsumer>();
                x.AddConsumer<CurriculumRemovedResponseConsumer>();

                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration["Rabbitmq_Host"], "/", cfg =>
                    {
                        cfg.Username(configuration["Rabbitmq_Username"]);
                        cfg.Password(configuration["Rabbitmq_Password"]);
                    });
                    configurator.ReceiveEndpoint("event-listener-core", e =>
                    {
                        e.ConfigureConsumer<CurriculumAddedRequestConsumer>(context);
                        e.ConfigureConsumer<CurriculumRemovedResponseConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}