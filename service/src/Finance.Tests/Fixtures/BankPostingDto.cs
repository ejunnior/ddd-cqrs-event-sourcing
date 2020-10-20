namespace Finance.Tests.Fixtures
{
    using Finance.Domain.Treasury.Aggregates.BankPostingAggregate;
    using System;

    public class BankPostingDto
    {
        public decimal Amount { get; set; }

        public Guid BankAccountId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid CreditorId { get; set; }

        public string Description { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public BankPostingType Type { get; set; }
    }
}