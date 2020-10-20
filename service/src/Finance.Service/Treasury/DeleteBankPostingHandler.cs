namespace Finance.Service.Treasury
{
    using Domain.Core;
    using MassTransit;
    using System.Threading.Tasks;
    using Domain.Treasury.Aggregates.BankPostingAggregate;

    public class DeleteBankPostingHandler
        : IConsumer<DeleteBankPostingCommand>
    {
        private readonly IBankPostingRepository _bankPostingRepository;

        public DeleteBankPostingHandler(
            IBankPostingRepository bankPostingRepository)
        {
            _bankPostingRepository = bankPostingRepository;
        }

        public async Task Consume(ConsumeContext<DeleteBankPostingCommand> context)
        {
            //var bankPosting = await _bankPostingRepository
            //    .GetAsync(context.Message.BankPostingId);

            //await _bankPostingRepository
            //    .RemoveAsync(bankPosting);
        }
    }
}