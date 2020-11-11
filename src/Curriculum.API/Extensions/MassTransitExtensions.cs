using Curriculum.API.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curriculum.API.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CurriculumAddedResponseConsumer>();
                x.AddConsumer<CurriculumCapacityCompletedConsumer>();
                x.AddConsumer<CurriculumCapacityFreedConsumer>();
                x.AddConsumer<CurriculumRemovedRequestConsumer>();
                x.AddConsumer<CurriculumUpdatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["Rabbitmq_Host"], "/", h =>
                    {
                        h.Username(configuration["Rabbitmq_Username"]);
                        h.Password(configuration["Rabbitmq_Password"]);
                    });
                    cfg.ReceiveEndpoint("event-listener-curriculum", e =>
                    {
                        e.ConfigureConsumer<CurriculumAddedResponseConsumer>(context);
                        e.ConfigureConsumer<CurriculumCapacityCompletedConsumer>(context);
                        e.ConfigureConsumer<CurriculumCapacityFreedConsumer>(context);
                        e.ConfigureConsumer<CurriculumRemovedRequestConsumer>(context);
                        e.ConfigureConsumer<CurriculumUpdatedConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}