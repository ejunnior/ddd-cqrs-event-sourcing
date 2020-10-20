namespace Finance.Tests.Treasury.Services
{
    using System;
    using System.Threading.Tasks;
    using Finance.Domain.Treasury.Aggregates.BankPostingAggregate;
    using Finance.Tests.Fixtures;
    using Finance.Tests.Infrastructure;
    using FluentAssertions;
    using Xunit;

    public class RegisterBankPostingHandlerTests
        : ServiceBaseTest<RegisterBankPostingCommand>
    {
        public RegisterBankPostingHandlerTests(FixtureBase fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldBankPostingBeRegistered()
        {
            // Arrange
            var creditor = new CreditorDtoFixture()
                .Build();

            await Connection
                .CreateCreditor(creditor);

            var category = new CategoryDtoFixture()
                .Build();

            await Connection
                .CreateCategory(category);

            var bankAccount = new BankAccountDtoFixture()
                .Build();

            await Connection
                .CreateBankAccount(bankAccount);

            var dto = new RegisterBankPostingDtoFixture()
                .WithCreditorId(creditor.Id)
                .WithCategoryId(category.Id)
                .WithBankAccountId(bankAccount.Id)
                .Build();

            // Act
            await SendMessage(new
            {
                Amount = dto.Amount,
                DueDate = dto.DueDate,
                DocumentDate = dto.DocumentDate,
                DocumentNumber = dto.DocumentNumber,
                CreditorId = dto.CreditorId,
                Description = dto.Description,
                BankAccountId = dto.BankAccountId,
                CategoryId = dto.CategoryId,
                PaymentDate = dto.PaymentDate,
                Type = dto.Type
            });

            // Assert
            var persisted = await Connection
                .GetBankPostingByDocumentNumber(dto.DocumentNumber);

            persisted
                .Amount
                .Should()
                .Be(dto.Amount);

            persisted
                .BankAccountId
                .Should()
                .Be(dto.BankAccountId);

            persisted
                .CategoryId
                .Should()
                .Be(dto.CategoryId);

            persisted
                .CreditorId
                .Should()
                .Be(dto.CreditorId);

            persisted
                .Description
                .Should()
                .Be(dto.Description);

            persisted
                .DocumentDate
                .Should()
                .Be(dto.DocumentDate);

            persisted
                .DocumentNumber
                .Should()
                .Be(dto.DocumentNumber);

            persisted
                .DueDate.Date
                .Should()
                .Be(dto.DueDate.Date);

            persisted
                .PaymentDate
                .Should()
                .Be(dto.PaymentDate);

            persisted
                .Type
                .Should()
                .Be(dto.Type);
        }
    }
}