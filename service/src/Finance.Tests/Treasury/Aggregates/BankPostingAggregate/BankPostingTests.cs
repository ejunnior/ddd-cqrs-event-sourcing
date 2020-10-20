namespace Finance.Tests.Treasury.Aggregates.BankPostingAggregate
{
    using System.Linq;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Fixtures;
    using FluentAssertions;
    using Xunit;

    public class BankPostingTests
    {
        [Fact]
        public void ShouldBankPostingBeRegistered()
        {
            // Arrange
            var dto = new RegisterBankPostingDtoFixture()
                .Build();

            // Act
            var bankPosting = BankPostingFactory.Create(
                amount: dto.Amount,
                dueDate: dto.DueDate,
                documentDate: dto.DocumentDate,
                documentNumber: dto.DocumentNumber,
                creditor: dto.CreditorId,
                description: dto.Description,
                bankAccount: dto.BankAccountId,
                category: dto.CategoryId,
                paymentDate: dto.PaymentDate,
                type: dto.Type).Value;

            // Assert
            bankPosting
                .Amount
                .Value
                .Should()
                .Be(dto.Amount);

            bankPosting
                .DueDate
                .Value
                .Should()
                .Be(dto.DueDate);

            bankPosting
                .DocumentDate
                .Value.Value
                .Should()
                .Be(dto.DocumentDate.Value);

            bankPosting
                .DocumentNumber
                .Value.Value
                .Should()
                .Be(dto.DocumentNumber);

            bankPosting
                .Creditor
                .Should()
                .Be(dto.CreditorId);

            bankPosting
                .Description
                .Value
                .Should()
                .Be(dto.Description);

            bankPosting
                .BankAccount
                .Should()
                .Be(dto.BankAccountId);

            bankPosting
                .Category
                .Should()
                .Be(dto.CategoryId);

            bankPosting
                .PaymentDate
                .Value.Value
                .Should()
                .Be(dto.PaymentDate.Value);

            bankPosting
                .Type
                .Should()
                .Be(dto.Type);

            bankPosting
                .DomainEvents
                .First().GetType()
                .Should()
                .Be(typeof(BankPostingRegisteredEvent));
        }
    }
}