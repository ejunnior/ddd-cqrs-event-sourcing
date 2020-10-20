namespace Finance.Domain.Core
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public static class DomainEvents
    {
        public static IServiceProvider Container { get; set; }

        public static async Task Raise<T>(T args) where T : IEvent
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(args.GetType());

            var handler = Container.GetRequiredService<IEventHandler<T>>();

            await handler.HandleAsync(args);
        }
    }
}