namespace DistributedServices.Subscriber.Bank
{
    using System.Threading.Tasks;
    using Application.Bank.Commands;
    using Domain.Core;
    using Infrastructure.Messages.Bank;
    using MassTransit;

    public class RegisterBankMessageHandler : IConsumer<RegisterBankMessage>
    {
        private readonly IDispatcher _dispatcher;

        public RegisterBankMessageHandler(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<RegisterBankMessage> context)
        {
            await _dispatcher
                .DispatchAsync(new RegisterBankCommand(context.Message.BankName));
        }
    }
}