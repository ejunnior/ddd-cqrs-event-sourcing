namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using System;
    using Core;

    public interface RegisterBankPostingCommand : ICommand
    {
        decimal Amount { get; }

        Guid BankAccountId { get; }

        Guid CategoryId { get; }

        Guid CreditorId { get; }

        string Description { get; }

        DateTime? DocumentDate { get; }

        string DocumentNumber { get; }

        DateTime DueDate { get; }

        DateTime? PaymentDate { get; }

        BankPostingType Type { get; } //TODO: need to be improved
    }
}