namespace Finance.Service.Treasury
{
    using System;
    using Domain.Core;
    using MassTransit;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Domain.Treasury.Aggregates.CategoryAggregate;

    public class EditBankPostingHandler
        : IConsumer<EditBankPostingCommand>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankPostingRepository _bankPostingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICreditorRepository _creditorRepository;

        public EditBankPostingHandler(
            IBankAccountRepository bankAccountRepository,
            IBankPostingRepository bankPostingRepository,
            ICategoryRepository categoryRepository,
            ICreditorRepository creditorRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankPostingRepository = bankPostingRepository;
            _categoryRepository = categoryRepository;
            _creditorRepository = creditorRepository;
        }

        public async Task Consume(ConsumeContext<EditBankPostingCommand> context)
        {
            if (!Enum.IsDefined(typeof(BankPostingType), //TODO : Review this code.
                context.Message.Type))
                throw new InvalidOperationException();

            var totalAmount = Amount.Create(context.Message.Amount);
            var dueDate = DueDate.Create(context.Message.DueDate);
            var documentDate = BankPostingFactory.GetDocumentDate(context.Message.DocumentDate);
            var documentNumber = BankPostingFactory.GetDocumentNumber(context.Message.DocumentNumber);
            var description = Description.Create(context.Message.Description);
            var paymentDate = BankPostingFactory.GetPaymentDate(context.Message.PaymentDate);

            var bankPosting = await _bankPostingRepository
                .GetAsync(Guid.Parse("68196873-c3b1-40b2-a057-c3c434904e7a"));

            //    var creditor = await _creditorRepository
            //        .GetAsync(context.Message.CreditorId);

            //    var bankAccount = await _bankAccountRepository
            //        .GetAsync(context.Message.BankAccountId);

            //    var category = await _categoryRepository
            //        .GetAsync(context.Message.CategoryId);

            bankPosting.Edit(
                amount: totalAmount.Value,
                dueDate: dueDate.Value,
                creditor: context.Message.CreditorId,
                description: description.Value,
                documentDate: documentDate.Value,
                documentNumber: documentNumber.Value,
                bankAccount: context.Message.BankAccountId,
                category: context.Message.CategoryId,
                paymentDate: paymentDate.Value,
                type: context.Message.Type);

            //await _bankPostingRepository
            //    .ModifyAsync(bankPosting);
        }
    }
}