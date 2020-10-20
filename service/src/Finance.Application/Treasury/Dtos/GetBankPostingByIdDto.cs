namespace Finance.Application.Treasury.Dtos
{
    using System;
    using Domain.Treasury.Aggregates.BankPostingAggregate;

    public class GetBankPostingByIdDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public Guid BankAccountId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid CreditorId { get; set; }

        public string Description { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public BankPostingType Type { get; set; } //TODO: this is not good. Should be improved
    }
}