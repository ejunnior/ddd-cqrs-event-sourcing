namespace Finance.Domain.Core
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Microsoft.Extensions.DependencyInjection;

    public class Dispatcher : IDispatcher
    {
        private readonly IServiceScope _serviceScope;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceScope = serviceProvider.CreateScope();
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery args)
            where TQuery : IQuery<TResult>
        {
            var handler = _serviceScope
                .ServiceProvider.GetService<IQueryHandler<TQuery, TResult>>();

            return await handler.HandleAsync(args);
        }

        public async Task DispatchAsync<T>(T args)
            where T : ICommand
        {
            var handler = _serviceScope
                .ServiceProvider.GetService<ICommandHandler<T>>();

            await handler.HandleAsync(args);
        }
    }
}