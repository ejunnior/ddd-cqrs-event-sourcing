namespace Finance.Tests.Treasury.Services
{
    using System.Reflection;
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Domain.Treasury.Aggregates.CategoryAggregate;
    using Finance.Infrastructure.Data.Bank.Repositories;
    using Finance.Infrastructure.Data.Creditor.Repositories;
    using Finance.Infrastructure.Data.Treasury.Repositories;
    using Finance.Infrastructure.Data.UnitOfWork;
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestDependencies(this IServiceCollection services)
        {
            return services
                .AddRepositories()
                .AddUnitOfWork()
                .AddHandlers();
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                var sources = scan.FromAssemblies(Assembly.Load("Finance.Service"));

                sources
                    .AddClasses(classes => classes.AssignableTo(typeof(IConsumer<>)))
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
            });

            return services;
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