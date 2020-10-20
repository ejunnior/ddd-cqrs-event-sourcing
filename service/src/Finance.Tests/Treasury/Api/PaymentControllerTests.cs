namespace Payment.Tests.Gateway.Api
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Threading.Tasks;
    using Finance.Tests.Fixtures.ReadModel;
    using Finance.Tests.Infrastructure;
    using FluentAssertions;
    using Xunit;

    public class PaymentControllerTests
        : ControllerBaseTest<FixtureBase>
    {
        public PaymentControllerTests(FixtureBase fixture)
            : base(fixture)
        {
        }

        private string Path => "api/v1/payment";

        [Fact]
        public async Task ShouldGetCreditCardTransaction()
        {
            // Arrange
            var dto = new CreditCardTransactionDtoFixture()
                .WithTransactionStatus("Approved")
                .Build();

            await Connection
                .CreateCreditCardTransaction(dto);

            var query = new NameValueCollection
            {
                {"transactionId", dto.MerchantTransactionId}
            };

            // Act
            var response = await HttpClient.GetAsync(GetUri(path: $"{Path}", query));

            // Assert
            //TODO: Room for improvements
            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }
    }
}