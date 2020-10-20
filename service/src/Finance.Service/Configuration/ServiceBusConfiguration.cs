namespace Finance.Service.Configuration
{
    using System;
    using System.Reflection;
    using GreenPipes;
    using MassTransit;
    using MassTransit.PrometheusIntegration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Exceptions;
    using Serilog.Sinks.Elasticsearch;

    public static class ServiceBusConfiguration
    {
        private static string ServiceName => "finance.service";

        public static IServiceCollection RegisterServiceBus(
            this IServiceCollection services
            )
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumers(Assembly.GetExecutingAssembly());

                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Environment.GetEnvironmentVariable("RabbitMQ__HostUri"), host =>
                    {
                        host.Username(Environment.GetEnvironmentVariable("RabbitMQ__Username"));
                        host.Password(Environment.GetEnvironmentVariable("RabbitMQ__Password"));
                    });

                    cfg.UseRetry(retryPolicy =>
                    {
                        retryPolicy.Incremental(5, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(100));
                    });

                    cfg.UseConsumeFilter(typeof(ConsumerFilter<>), context);

                    //TODO: Room for improvement
                    var loggerInstance = ConfigureLogging();
                    cfg.ConnectConsumeObserver(new ConsumerObserver(loggerInstance));
                    //TODO: Room for improvement

                    cfg.ReceiveEndpoint(
                        queueName: ServiceName,
                        configureEndpoint: endPoint =>
                        {
                            endPoint.ConfigureConsumers(context);
                        });

                    cfg.UsePrometheusMetrics(serviceName: ServiceName);
                });
            });

            return services;
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var elasticSearchUri = Environment.GetEnvironmentVariable("ElasticConfiguration__Uri");

            return new ElasticsearchSinkOptions(new Uri(elasticSearchUri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }

        private static Serilog.ILogger ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink())
                .Enrich.WithProperty("Environment", environment)
                .CreateLogger();
        }
    }
}