namespace Finance.Tests.Fixtures
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Bank.Aggregates.BankAccountAggregate;

    public class BankAccountDto
    {
        [StringLength(20)]
        public string AccountNumber { get; set; }

        public Bank Bank { get; set; }

        public Guid Id { get; set; }
    }
}