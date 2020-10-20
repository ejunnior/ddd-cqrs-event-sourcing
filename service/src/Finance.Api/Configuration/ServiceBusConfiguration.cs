namespace Finance.Api.Configuration
{
    using System;
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfiguration
    {
        public static IServiceCollection RegisterServiceBus(
            this IServiceCollection services)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddBus(provider =>
                    SetupRabbitMq());
            });

            return services;
        }

        private static IBusControl SetupRabbitMq()
        {
            return Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host(new Uri(Environment.GetEnvironmentVariable("RabbitMQ__HostUri")), host =>
                {
                    host.Username(Environment.GetEnvironmentVariable("RabbitMQ__Username"));
                    host.Password(Environment.GetEnvironmentVariable("RabbitMQ__Password"));
                });
            });
        }
    }
}