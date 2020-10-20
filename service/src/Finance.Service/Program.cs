namespace Finance.Service
{
    using Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Reflection;
    using Serilog;
    using Serilog.Exceptions;
    using Serilog.Sinks.Elasticsearch;

    internal class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddDependencies()
                        .RegisterServiceBus()
                        .AddHostedService<Service>();
                });

        public static void Main(string[] args)
        {
            //TODO: Room for improvement
            ConfigureLogging();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", e);
                throw;
            }
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

        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Log.Logger = new LoggerConfiguration()
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