namespace Finance.Service
{
    using System;
    using System.Threading.Tasks;
    using GreenPipes;
    using Infrastructure.Data.UnitOfWork;
    using MassTransit;
    using MassTransit.Scoping;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    //TODO: Room form improvements
    public class ConsumerFilter<T>
        : IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly IConsumerScopeProvider _consumerScopeProvider;
        private readonly ILogger<IConsumerScopeProvider> _logger;

        public ConsumerFilter(
            IConsumerScopeProvider consumerScopeProvider,
            ILogger<IConsumerScopeProvider> logger)
        {
            _consumerScopeProvider = consumerScopeProvider;
            _logger = logger;
        }

        private IFinanceUnitOfWork UnitOfWork { get; set; }

        public void Probe(ProbeContext context)
        {
        }

        public async Task Send(
            ConsumeContext<T> context,
            IPipe<ConsumeContext<T>> next)
        {
            var scope = _consumerScopeProvider.GetScope(context);

            var serviceScope = scope
                .Context.GetPayload<IServiceScope>();

            var serviceProviderScoped = serviceScope
                .ServiceProvider;

            UnitOfWork = serviceProviderScoped
                .GetService<IFinanceUnitOfWork>();

            await next.Send(context);

            await UnitOfWork.SaveChangesAsync();
        }
    }
}