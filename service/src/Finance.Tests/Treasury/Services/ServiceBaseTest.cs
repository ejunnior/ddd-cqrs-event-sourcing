namespace Finance.Tests.Treasury.Services
{
    using System;
    using System.Threading.Tasks;
    using Finance.Infrastructure.Data.UnitOfWork;
    using Infrastructure;
    using MassTransit;
    using MassTransit.Testing;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    [Collection(FixtureBaseCollection.Name)]
    public abstract class ServiceBaseTest<TMessage>
        : IDisposable
        where TMessage : class
    {
        protected readonly InMemoryTestHarness _harness;
        protected ConsumerTestHarness<IConsumer<TMessage>> _consumer;
        private readonly FixtureBase _fixture;
        private readonly IServiceScope _serviceScope;

        private readonly IFinanceUnitOfWork _unitOfWork;
        private IConsumer<TMessage> _handler;

        protected ServiceBaseTest(FixtureBase fixture)
        {
            _fixture = fixture;

            _serviceScope = _fixture
                .ServiceHostServices.CreateScope();

            _harness = new InMemoryTestHarness();

            _handler = _serviceScope.ServiceProvider.GetService<IConsumer<TMessage>>();

            //TODO: Room for improvement
            _unitOfWork = _serviceScope.ServiceProvider.GetService<IFinanceUnitOfWork>();

            _consumer = _harness.Consumer(() => _handler);

            new Action(async () => { await _harness.Start(); }).Invoke();

            Connection = fixture.Connection;
        }

        protected MockServices MockServices => _fixture.MockServices;

        protected SqlConnection Connection { get; }

        public void Dispose()
        {
            new Action(async () => { await _harness.Stop(); }).Invoke();
            _harness.Dispose();
        }

        protected async Task AssertMessageReceived()
        {
            await _harness.Consumed.Any<TMessage>();
            await _consumer.Consumed.Any<TMessage>();
        }

        protected async Task SendMessage(object message)
        {
            //try
            //{
            //    _unitOfWork.BeginTransaction();

            //    _harness.InputQueueSendEndpoint
            //       .Send<TMessage>(message).Wait();

            //    await AssertMessageReceived();

            //    await _unitOfWork.CommitAsync();
            //}
            //catch (Exception e)
            //{
            //    await _unitOfWork.RollbackChangesAsync();
            //    throw;
            //}
        }
    }
}