namespace Finance.Tests.Fixtures
{
    using System;
    using AutoFixture;
    using Domain.Treasury.Aggregates.BankPostingAggregate;

    internal class RegisterBankPostingDtoFixture
    {
        private readonly RegisterBankPostingDto _dto;

        public RegisterBankPostingDtoFixture()
        {
            var fixture = new Fixture();
            _dto = fixture.Create<RegisterBankPostingDto>();
        }

        public RegisterBankPostingDto Build()
        {
            return _dto;
        }

        public RegisterBankPostingDtoFixture WithAmount(decimal amount)
        {
            _dto.Amount = amount;
            return this;
        }

        public RegisterBankPostingDtoFixture WithBankAccountId(Guid bankAccountId)
        {
            _dto.BankAccountId = bankAccountId;
            return this;
        }

        public RegisterBankPostingDtoFixture WithCategoryId(Guid categoryId)
        {
            _dto.CategoryId = categoryId;
            return this;
        }

        public RegisterBankPostingDtoFixture WithCreditorId(Guid creditorId)
        {
            _dto.CreditorId = creditorId;
            return this;
        }

        public RegisterBankPostingDtoFixture WithDescription(string description)
        {
            _dto.Description = description;
            return this;
        }

        public RegisterBankPostingDtoFixture WithDocumentDate(DateTime? documentDate)
        {
            _dto.DocumentDate = documentDate;
            return this;
        }

        public RegisterBankPostingDtoFixture WithDocumentNumber(string documentNumber)
        {
            _dto.DocumentNumber = documentNumber;
            return this;
        }

        public RegisterBankPostingDtoFixture WithDueDate(DateTime dueDate)
        {
            _dto.DueDate = dueDate;
            return this;
        }

        public RegisterBankPostingDtoFixture WithPaymentDate(DateTime paymentDate)
        {
            _dto.PaymentDate = paymentDate;
            return this;
        }

        public RegisterBankPostingDtoFixture WithType(BankPostingType type)
        {
            _dto.Type = type;
            return this;
        }
    }
}