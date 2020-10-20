namespace Finance.Tests.Treasury.Aggregates.BankPostingAggregate
{
    using Domain;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Fixtures;
    using FluentAssertions;
    using Xunit;

    public class AmountTests
    {
        [Fact]
        public void ShouldAmountBeCreated()
        {
            // Arrange
            var dto = new AmountDtoFixture()
                .WithValue(100)
                .Build();

            // Act
            var amount = Amount
                .Create(dto.Value);

            // Assert
            amount
                .IsSuccess
                .Should()
                .Be(true);

            amount
                .Value
                .Value
                .Should().Be(dto.Value);
        }

        [Fact]
        public void ShouldAmountNotBeCreated()
        {
            // Arrange
            var dto = new AmountDtoFixture()
                .WithValue(-1)
                .Build();

            // Act
            var amount = Amount
                .Create(dto.Value);

            // Assert
            amount
                .IsFailure
                .Should()
                .Be(true);

            amount
                .Error
                .Should()
                .Be(Errors.General.ValueIsInvalid());
        }
    }
}