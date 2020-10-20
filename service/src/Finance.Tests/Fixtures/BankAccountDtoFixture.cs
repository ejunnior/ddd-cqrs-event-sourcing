namespace Finance.Tests.Fixtures
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoFixture;
    using Domain.Bank.Aggregates.BankAccountAggregate;

    public class BankAccountDtoFixture
    {
        private readonly BankAccountDto _dto;

        public BankAccountDtoFixture()
        {
            var fixture = new Fixture();
            _dto = fixture.Create<BankAccountDto>();
        }

        public BankAccountDto Build()
        {
            return _dto;
        }

        public BankAccountDtoFixture WithAccountNumber(string accountNumber)
        {
            _dto.AccountNumber = accountNumber;
            return this;
        }

        public BankAccountDtoFixture WithBank(Bank bank)
        {
            _dto.Bank = bank;
            return this;
        }

        public BankAccountDtoFixture WithId(Guid id)
        {
            _dto.Id = id;
            return this;
        }
    }
}