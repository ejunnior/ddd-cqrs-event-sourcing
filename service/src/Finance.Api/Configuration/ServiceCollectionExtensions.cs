namespace Finance.Api.Configuration
{
    using System.Reflection;
    using Domain.Core;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            return services
                .AddDispatcher()
                .AddQueryHandlers();
        }

        private static IServiceCollection AddDispatcher(this IServiceCollection services)
        {
            return services.AddSingleton<IDispatcher, Dispatcher>();
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                var sources = scan.FromAssemblies(Assembly.Load("Finance.Application"));

                sources
                    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
            });

            return services;
        }
    }
}