namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using System;
    using Core;

    public interface DeleteBankPostingCommand : ICommand
    {
        Guid BankPostingId { get; }
    }
}