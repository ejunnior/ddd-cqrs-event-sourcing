namespace DistributedServices.Subscriber.Bank
{
    using System.Threading.Tasks;
    using Application.Bank.Commands;
    using Domain.Core;
    using Infrastructure.Messages.Bank;
    using MassTransit;

    public class RegisterBankAccountMessageHandler
        : IConsumer<RegisterBankAccountMessage>
    {
        private readonly IDispatcher _dispatcher;

        public RegisterBankAccountMessageHandler(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<RegisterBankAccountMessage> context)
        {
            await _dispatcher
                .DispatchAsync(new RegisterBankAccountCommand(
                    accountNumber: context.Message.AccountNumber,
                    initialBalanceAmount: context.Message.InitialBalanceAmount,
                    bankId: context.Message.BankId));
        }
    }
}