namespace Finance.Service.Configuration
{
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using Domain.Core;
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Domain.Treasury.Aggregates.CategoryAggregate;
    using Infrastructure.Data.Bank.Repositories;
    using Infrastructure.Data.Creditor.Repositories;
    using Infrastructure.Data.Treasury.Repositories;
    using Infrastructure.Data.UnitOfWork;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            return services
                .AddRepositories()
                .AddDispatcher()
                .AddUnitOfWork()
                .AddCommandHandlers()
                .AddFilters();
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                var sources = scan.FromAssemblies(Assembly.Load("Finance.Application"));

                sources
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
            });

            return services;
        }

        private static IServiceCollection AddDispatcher(this IServiceCollection services)
        {
            return services.AddSingleton<IDispatcher, Dispatcher>();
        }

        private static IServiceCollection AddFilters(this IServiceCollection services)
        {
            return services.AddScoped(typeof(ConsumerFilter<>));
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICreditorRepository, CreditorRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IBankPostingRepository, BankPostingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IFinanceUnitOfWork, FinanceUnitOfWork>();
        }
    }
}