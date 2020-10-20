namespace Finance.Application.Treasury.Dtos
{
    using System;
    using Domain.Treasury.Aggregates.BankPostingAggregate;

    public class GetBankPostingDto
    {
        //public decimal Amount { get; set; }

        //public string Creditor { get; set; }
        //public string Description { get; set; }

        //public DateTime? DocumentDate { get; set; }

        //public string DocumentNumber { get; set; }

        //public DateTime DueDate { get; set; }

        //public Guid Id { get; set; }

        //public DateTime? PaymentDate { get; set; }

        //public BankPostingType Type { get; set; } //TODO: this is not good. Should be improved
        public decimal Amount { get; set; }

        public string BankAccount { get; set; }

        public string Category { get; set; }

        public string Creditor { get; set; }

        public string Description { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DueDate { get; set; }

        public Guid Id { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime PostingDate { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }
}